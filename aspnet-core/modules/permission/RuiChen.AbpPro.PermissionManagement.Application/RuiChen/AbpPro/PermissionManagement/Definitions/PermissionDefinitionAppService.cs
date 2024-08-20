using Microsoft.AspNetCore.Authorization;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Authorization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Localization;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SimpleStateChecking;
using Volo.Abp.Validation;

namespace RuiChen.AbpPro.PermissionManagement
{
    public class PermissionDefinitionAppService : PermissionManagementAppServiceBase, IPermissionDefinitionAppService
    {
        private readonly ILocalizableStringSerializer localizableStringSerializer;
        private readonly IPermissionDefinitionManager permissionDefinitionManager;
        private readonly IStaticPermissionDefinitionStore staticPermissionDefinitionStore;
        private readonly IDynamicPermissionDefinitionStore dynamicPermissionDefinitionStore;
        private readonly ISimpleStateCheckerSerializer simpleStateCheckerSerializer;
        private readonly IPermissionDefinitionRecordRepository definitionRepository;
        private readonly IRepository<PermissionDefinitionRecord, Guid> definitionBasicRepository;

        public PermissionDefinitionAppService(
            ILocalizableStringSerializer localizableStringSerializer,
            IPermissionDefinitionManager permissionDefinitionManager,
            IStaticPermissionDefinitionStore staticPermissionDefinitionStore,
            IDynamicPermissionDefinitionStore dynamicPermissionDefinitionStore,
            ISimpleStateCheckerSerializer simpleStateCheckerSerializer,
            IPermissionDefinitionRecordRepository definitionRepository,
            IRepository<PermissionDefinitionRecord, Guid> definitionBasicRepository)
        {
            this.localizableStringSerializer = localizableStringSerializer;
            this.permissionDefinitionManager = permissionDefinitionManager;
            this.staticPermissionDefinitionStore = staticPermissionDefinitionStore;
            this.dynamicPermissionDefinitionStore = dynamicPermissionDefinitionStore;
            this.simpleStateCheckerSerializer = simpleStateCheckerSerializer;
            this.definitionRepository = definitionRepository;
            this.definitionBasicRepository = definitionBasicRepository;
        }


        [Authorize(PermissionManagementPermissionNames.Definition.Create)]
        public async virtual Task<PermissionDefinitionDto> CreateAsync(PermissionDefinitionCreateDto input)
        {
            if (await permissionDefinitionManager.GetOrNullAsync(input.Name) != null)
            {
                throw new BusinessException(PermissionManagementErrorCodes.Definition.AlreayNameExists)
                    .WithData(nameof(PermissionDefinitionRecord.Name), input.Name);
            }
            var staticGroups = await staticPermissionDefinitionStore.GetGroupsAsync();
            if (staticGroups.Any(g => g.Name == input.GroupName))
            {
                throw new BusinessException(PermissionManagementErrorCodes.GroupDefinition.StaticGroupNotAllowedChanged)
                    .WithData(nameof(PermissionDefinitionRecord.Name), input.GroupName);
            }

            var groupDefinition = await permissionDefinitionManager.GetGroupOrNullAsync(input.GroupName);
            if (groupDefinition == null)
            {
                throw new BusinessException(PermissionManagementErrorCodes.GroupDefinition.NameNotFount)
                    .WithData(nameof(PermissionGroupDefinitionRecord.Name), input.GroupName);
            }

            var definitionRecord = new PermissionDefinitionRecord(
                GuidGenerator.Create(),
                groupDefinition.Name,
                input.Name,
                input.ParentName,
                input.DisplayName,
                input.IsEnabled);

            await UpdateByInput(definitionRecord, input);

            definitionRecord = await definitionRepository.InsertAsync(definitionRecord);

            await CurrentUnitOfWork.SaveChangesAsync();

            return DefinitionRecordToDto(definitionRecord);
        }

        [Authorize(PermissionManagementPermissionNames.Definition.Delete)]
        public async virtual Task DeleteAsync(string name)
        {
            var definitionRecord = await FindByNameAsync(name);

            if (definitionRecord != null)
            {
                await definitionRepository.DeleteAsync(definitionRecord);

                await CurrentUnitOfWork.SaveChangesAsync();
            }
        }

        public async virtual Task<PermissionDefinitionDto> GetAsync(string name)
        {
            var definition = await staticPermissionDefinitionStore.GetOrNullAsync(name);
            if (definition != null)
            {
                return DefinitionToDto(await GetGroupDefinition(definition), definition, true);
            }
            definition = await dynamicPermissionDefinitionStore.GetOrNullAsync(name);
            return DefinitionToDto(await GetGroupDefinition(definition), definition);
        }

        public async virtual Task<ListResultDto<PermissionDefinitionDto>> GetListAsync(PermissionDefinitionGetListInput input)
        {
            var permissionDtoList = new List<PermissionDefinitionDto>();

            var staticPermissions = new List<PermissionDefinition>();

            var staticGroups = await staticPermissionDefinitionStore.GetGroupsAsync();
            var staticGroupNames = staticGroups
                .Select(p => p.Name)
                .ToImmutableHashSet();
            foreach (var group in staticGroups.WhereIf(!input.GroupName.IsNullOrWhiteSpace(), x => x.Name == input.GroupName))
            {
                var permissions = group.GetPermissionsWithChildren();
                staticPermissions.AddRange(permissions);
                permissionDtoList.AddRange(permissions.Select(f => DefinitionToDto(group, f, true)));
            }
            var staticPermissionNames = staticPermissions
                .Select(p => p.Name)
                .ToImmutableHashSet();
            var dynamicGroups = await dynamicPermissionDefinitionStore.GetGroupsAsync();
            foreach (var group in dynamicGroups
                .Where(d => !staticGroupNames.Contains(d.Name))
                .WhereIf(!input.GroupName.IsNullOrWhiteSpace(), x => x.Name == input.GroupName))
            {
                var permissions = group.GetPermissionsWithChildren();
                permissionDtoList.AddRange(permissions
                    .Where(d => !staticPermissionNames.Contains(d.Name))
                    .Select(f => DefinitionToDto(group, f)));
            }

            return new ListResultDto<PermissionDefinitionDto>(permissionDtoList
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Filter) || x.DisplayName.Contains(input.Filter))
                .ToList());
        }

        [Authorize(PermissionManagementPermissionNames.Definition.Update)]
        public async virtual Task<PermissionDefinitionDto> UpdateAsync(string name, PermissionDefinitionUpdateDto input)
        {
            if (await staticPermissionDefinitionStore.GetOrNullAsync(name) != null)
            {
                throw new BusinessException(PermissionManagementErrorCodes.Definition.StaticPermissionNotAllowedChanged)
                  .WithData("Name", name);
            }

            var definition = await permissionDefinitionManager.GetOrNullAsync(name);
            var definitionRecord = await FindByNameAsync(name);

            if (definitionRecord == null)
            {
                var groupDefinition = await GetGroupDefinition(definition);
                definitionRecord = new PermissionDefinitionRecord(
                    GuidGenerator.Create(),
                    groupDefinition.Name,
                    name,
                    input.ParentName,
                    input.DisplayName,
                    input.IsEnabled);

                await UpdateByInput(definitionRecord, input);

                definitionRecord = await definitionBasicRepository.InsertAsync(definitionRecord);
            }
            else
            {
                await UpdateByInput(definitionRecord, input);

                definitionRecord = await definitionBasicRepository.UpdateAsync(definitionRecord);
            }

            await CurrentUnitOfWork.SaveChangesAsync();

            return DefinitionRecordToDto(definitionRecord);
        }

        protected async virtual Task UpdateByInput(PermissionDefinitionRecord record, PermissionDefinitionCreateOrUpdateDto input)
        {
            record.IsEnabled = input.IsEnabled;
            record.MultiTenancySide = input.MultiTenancySide;
            if (!string.Equals(record.ParentName, input.ParentName, StringComparison.InvariantCultureIgnoreCase))
            {
                record.ParentName = input.ParentName;
            }
            if (!string.Equals(record.DisplayName, input.DisplayName, StringComparison.InvariantCultureIgnoreCase))
            {
                record.DisplayName = input.DisplayName;
            }
            string providers = null;
            if (!input.Providers.IsNullOrEmpty())
            {
                providers = input.Providers.JoinAsString(",");
            }
            if (!string.Equals(record.Providers, providers, StringComparison.InvariantCultureIgnoreCase))
            {
                record.Providers = providers;
            }

            record.ExtraProperties.Clear();
            foreach (var property in input.ExtraProperties)
            {
                record.SetProperty(property.Key, property.Value);
            }

            try
            {
                if (!string.Equals(record.StateCheckers, input.StateCheckers, StringComparison.InvariantCultureIgnoreCase))
                {
                    // 校验格式
                    var permissionDefinition = await staticPermissionDefinitionStore.GetOrNullAsync(PermissionManagementPermissionNames.Definition.Default);
                    var _ = simpleStateCheckerSerializer.DeserializeArray(input.StateCheckers, permissionDefinition);

                    record.StateCheckers = input.StateCheckers;
                }
            }
            catch
            {
                throw new AbpValidationException(
                    new List<ValidationResult>
                    {
                    new ValidationResult(
                        L["The field {0} is invalid", L["DisplayName:StateCheckers"]],
                        new string[1] { nameof(input.StateCheckers) })
                    });
            }
        }

        protected async virtual Task<PermissionDefinitionRecord> FindByNameAsync(string name)
        {
            var DefinitionFilter = await definitionBasicRepository.GetQueryableAsync();

            var definitionRecord = await AsyncExecuter.FirstOrDefaultAsync(
                DefinitionFilter.Where(x => x.Name == name));

            return definitionRecord;
        }

        protected async virtual Task<PermissionGroupDefinition> GetGroupDefinition(PermissionDefinition definition)
        {
            var groups = await permissionDefinitionManager.GetGroupsAsync();

            foreach (var group in groups)
            {
                if (group.GetPermissionOrNull(definition.Name) != null)
                {
                    return group;
                }
            }

            throw new BusinessException(PermissionManagementErrorCodes.Definition.FailedGetGroup)
                .WithData(nameof(PermissionDefinitionRecord.Name), definition.Name);
        }

        protected virtual PermissionDefinitionDto DefinitionRecordToDto(PermissionDefinitionRecord definitionRecord)
        {
            var dto = new PermissionDefinitionDto
            {
                IsStatic = false,
                Name = definitionRecord.Name,
                GroupName = definitionRecord.GroupName,
                ParentName = definitionRecord.ParentName,
                IsEnabled = definitionRecord.IsEnabled,
                DisplayName = definitionRecord.DisplayName,
                Providers = definitionRecord.Providers?.Split(',').ToList(),
                StateCheckers = definitionRecord.StateCheckers,
                MultiTenancySide = definitionRecord.MultiTenancySide,
                ExtraProperties = new ExtraPropertyDictionary(),
            };

            foreach (var property in definitionRecord.ExtraProperties)
            {
                dto.SetProperty(property.Key, property.Value);
            }

            return dto;
        }

        protected virtual PermissionDefinitionDto DefinitionToDto(PermissionGroupDefinition groupDefinition, PermissionDefinition definition, bool isStatic = false)
        {
            var dto = new PermissionDefinitionDto
            {
                IsStatic = isStatic,
                Name = definition.Name,
                GroupName = groupDefinition.Name,
                ParentName = definition.Parent?.Name,
                IsEnabled = definition.IsEnabled,
                Providers = definition.Providers,
                MultiTenancySide = definition.MultiTenancySide,
                DisplayName = localizableStringSerializer.Serialize(definition.DisplayName),
                ExtraProperties = new ExtraPropertyDictionary(),
            };

            if (definition.StateCheckers.Any())
            {
                dto.StateCheckers = simpleStateCheckerSerializer.Serialize(definition.StateCheckers);
            }

            foreach (var property in definition.Properties)
            {
                dto.SetProperty(property.Key, property.Value);
            }

            return dto;
        }

    }
}
