﻿using Microsoft.AspNetCore.Authorization;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Features;
using Volo.Abp.Localization;
using Volo.Abp.Validation;

namespace RuiChen.AbpPro.FeatureManagement
{
    public class FeatureDefinitionAppService : FeatureManagementAppServiceBase, IFeatureDefinitionAppService
    {
        private readonly StringValueTypeSerializer stringValueTypeSerializer;
        private readonly ILocalizableStringSerializer localizableStringSerializer;
        private readonly IFeatureDefinitionManager featureDefinitionManager;
        private readonly IStaticFeatureDefinitionStore staticFeatureDefinitionStore;
        private readonly IDynamicFeatureDefinitionStore dynamicFeatureDefinitionStore;
        private readonly IFeatureDefinitionRecordRepository definitionRepository;
        private readonly IRepository<FeatureDefinitionRecord, Guid> definitionBasicRepository;

        public FeatureDefinitionAppService(
            StringValueTypeSerializer stringValueTypeSerializer,
            ILocalizableStringSerializer localizableStringSerializer,
            IFeatureDefinitionManager featureDefinitionManager,
            IStaticFeatureDefinitionStore staticFeatureDefinitionStore,
            IDynamicFeatureDefinitionStore dynamicFeatureDefinitionStore,
            IFeatureDefinitionRecordRepository definitionRepository,
            IRepository<FeatureDefinitionRecord, Guid> definitionBasicRepository)
        {
            this.stringValueTypeSerializer = stringValueTypeSerializer;
            this.localizableStringSerializer = localizableStringSerializer;
            this.featureDefinitionManager = featureDefinitionManager;
            this.staticFeatureDefinitionStore = staticFeatureDefinitionStore;
            this.dynamicFeatureDefinitionStore = dynamicFeatureDefinitionStore;
            this.definitionRepository = definitionRepository;
            this.definitionBasicRepository = definitionBasicRepository;
        }


        [Authorize(FeatureManagementPermissionNames.Definition.Create)]
        public async virtual Task<FeatureDefinitionDto> CreateAsync(FeatureDefinitionCreateDto input)
        {
            var staticGroups = await staticFeatureDefinitionStore.GetGroupsAsync();
            if (staticGroups.Any(g => g.Name == input.GroupName))
            {
                throw new BusinessException(FeatureManagementErrorCodes.GroupDefinition.StaticGroupNotAllowedChanged)
                    .WithData(nameof(FeatureDefinitionRecord.Name), input.GroupName);
            }

            if (await staticFeatureDefinitionStore.GetOrNullAsync(input.Name) != null)
            {
                throw new BusinessException(FeatureManagementErrorCodes.Definition.AlreayNameExists)
                    .WithData(nameof(FeatureDefinitionRecord.Name), input.Name);
            }

            if (await definitionRepository.FindByNameAsync(input.Name) != null)
            {
                throw new BusinessException(FeatureManagementErrorCodes.Definition.AlreayNameExists)
                    .WithData(nameof(FeatureDefinitionRecord.Name), input.Name);
            }

            var groupDefinition = await featureDefinitionManager.GetGroupOrNullAsync(input.GroupName);
            if (groupDefinition == null)
            {
                throw new BusinessException(FeatureManagementErrorCodes.GroupDefinition.NameNotFount)
                    .WithData(nameof(FeatureGroupDefinitionRecord.Name), input.GroupName);
            }

            var definitionRecord = new FeatureDefinitionRecord(
                GuidGenerator.Create(),
                groupDefinition.Name,
                input.Name,
                input.ParentName,
                input.DisplayName,
                input.Description,
                input.DefaultValue,
                input.IsVisibleToClients,
                input.IsAvailableToHost,
                valueType: input.ValueType);

            UpdateByInput(definitionRecord, input);

            await definitionRepository.InsertAsync(definitionRecord);

            await CurrentUnitOfWork.SaveChangesAsync();

            return DefinitionRecordToDto(definitionRecord);
        }

        [Authorize(FeatureManagementPermissionNames.Definition.Delete)]
        public async virtual Task DeleteAsync(string name)
        {
            var definitionRecord = await FindRecordByNameAsync(name);

            if (definitionRecord != null)
            {
                await definitionRepository.DeleteAsync(definitionRecord);

                await CurrentUnitOfWork.SaveChangesAsync();
            }
        }

        public async virtual Task<FeatureDefinitionDto> GetAsync(string name)
        {
            var definition = await staticFeatureDefinitionStore.GetOrNullAsync(name);
            if (definition != null)
            {
                return DefinitionToDto(await GetGroupDefinition(definition), definition, true);
            }
            definition = await dynamicFeatureDefinitionStore.GetOrNullAsync(name);
            return DefinitionToDto(await GetGroupDefinition(definition), definition);
        }

        public async virtual Task<ListResultDto<FeatureDefinitionDto>> GetListAsync(FeatureDefinitionGetListInput input)
        {
            var featureDtoList = new List<FeatureDefinitionDto>();

            var staticFreatures = new List<FeatureDefinition>();

            var staticGroups = await staticFeatureDefinitionStore.GetGroupsAsync();
            var staticGroupNames = staticGroups
                .Select(p => p.Name)
                .ToImmutableHashSet();
            foreach (var group in staticGroups.WhereIf(!input.GroupName.IsNullOrWhiteSpace(), x => x.Name == input.GroupName))
            {
                var features = group.GetFeaturesWithChildren();
                staticFreatures.AddRange(features);
                featureDtoList.AddRange(features.Select(f => DefinitionToDto(group, f, true)));
            }
            var staticFeatureNames = staticFreatures
                .Select(p => p.Name)
                .ToImmutableHashSet();
            var dynamicGroups = await dynamicFeatureDefinitionStore.GetGroupsAsync();
            foreach (var group in dynamicGroups
                .Where(d => !staticGroupNames.Contains(d.Name))
                .WhereIf(!input.GroupName.IsNullOrWhiteSpace(), x => x.Name == input.GroupName))
            {
                var features = group.GetFeaturesWithChildren();
                featureDtoList.AddRange(features
                    .Where(d => !staticFeatureNames.Contains(d.Name))
                    .Select(f => DefinitionToDto(group, f)));
            }

            return new ListResultDto<FeatureDefinitionDto>(featureDtoList
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Filter) || x.DisplayName.Contains(input.Filter))
                .ToList());
        }

        [Authorize(FeatureManagementPermissionNames.Definition.Update)]
        public async virtual Task<FeatureDefinitionDto> UpdateAsync(string name, FeatureDefinitionUpdateDto input)
        {
            if (await staticFeatureDefinitionStore.GetOrNullAsync(name) != null)
            {
                throw new BusinessException(FeatureManagementErrorCodes.Definition.StaticFeatureNotAllowedChanged)
                  .WithData("Name", name);
            }

            var definition = await featureDefinitionManager.GetAsync(name);
            var definitionRecord = await FindRecordByNameAsync(name);

            if (definitionRecord == null)
            {
                var groupDefinition = await GetGroupDefinition(definition);
                definitionRecord = new FeatureDefinitionRecord(
                    GuidGenerator.Create(),
                    groupDefinition.Name,
                    name,
                    input.ParentName,
                    input.DisplayName,
                    input.Description,
                    input.DefaultValue,
                    input.IsVisibleToClients,
                    input.IsAvailableToHost,
                    valueType: input.ValueType);
                UpdateByInput(definitionRecord, input);

                definitionRecord = await definitionBasicRepository.InsertAsync(definitionRecord);
            }
            else
            {
                UpdateByInput(definitionRecord, input);
                definitionRecord = await definitionBasicRepository.UpdateAsync(definitionRecord);
            }

            await CurrentUnitOfWork.SaveChangesAsync();

            return DefinitionRecordToDto(definitionRecord);
        }

        protected virtual void UpdateByInput(FeatureDefinitionRecord record, FeatureDefinitionCreateOrUpdateDto input)
        {
            record.IsVisibleToClients = input.IsVisibleToClients;
            record.IsAvailableToHost = input.IsAvailableToHost;
            if (!string.Equals(record.ParentName, input.ParentName, StringComparison.InvariantCultureIgnoreCase))
            {
                record.ParentName = input.ParentName;
            }
            if (!string.Equals(record.DisplayName, input.DisplayName, StringComparison.InvariantCultureIgnoreCase))
            {
                record.DisplayName = input.DisplayName;
            }
            if (!string.Equals(record.Description, input.Description, StringComparison.InvariantCultureIgnoreCase))
            {
                record.Description = input.Description;
            }
            if (!string.Equals(record.DefaultValue, input.DefaultValue, StringComparison.InvariantCultureIgnoreCase))
            {
                record.DefaultValue = input.DefaultValue;
            }
            string allowedProviders = null;
            if (!input.AllowedProviders.IsNullOrEmpty())
            {
                allowedProviders = input.AllowedProviders.JoinAsString(",");
            }
            if (!string.Equals(record.AllowedProviders, allowedProviders, StringComparison.InvariantCultureIgnoreCase))
            {
                record.AllowedProviders = allowedProviders;
            }
            record.ExtraProperties.Clear();
            foreach (var property in input.ExtraProperties)
            {
                record.SetProperty(property.Key, property.Value);
            }
            try
            {
                if (!string.Equals(record.ValueType, input.ValueType, StringComparison.InvariantCultureIgnoreCase))
                {
                    var _ = stringValueTypeSerializer.Deserialize(input.ValueType);
                    record.ValueType = input.ValueType;
                }
            }
            catch
            {
                throw new AbpValidationException(
                    new List<ValidationResult>
                    {
                    new ValidationResult(
                        L["The field {0} is invalid", L["DisplayName:ValueType"]],
                        new string[1] { nameof(input.ValueType) })
                    });
            }
        }

        protected async virtual Task<FeatureDefinitionRecord> FindRecordByNameAsync(string name)
        {
            var DefinitionFilter = await definitionBasicRepository.GetQueryableAsync();

            var definitionRecord = await AsyncExecuter.FirstOrDefaultAsync(
                DefinitionFilter.Where(x => x.Name == name));

            return definitionRecord;
        }

        protected async virtual Task<FeatureGroupDefinition> GetGroupDefinition(FeatureDefinition definition)
        {
            var groups = await featureDefinitionManager.GetGroupsAsync();

            foreach (var group in groups)
            {
                if (group.GetFeatureOrNull(definition.Name) != null)
                {
                    return group;
                }
            }

            throw new BusinessException(FeatureManagementErrorCodes.Definition.FailedGetGroup)
                .WithData(nameof(FeatureDefinitionRecord.Name), definition.Name);
        }

        protected virtual FeatureDefinitionDto DefinitionRecordToDto(FeatureDefinitionRecord definitionRecord)
        {
            var dto = new FeatureDefinitionDto
            {
                Name = definitionRecord.Name,
                GroupName = definitionRecord.GroupName,
                ParentName = definitionRecord.ParentName,
                IsAvailableToHost = definitionRecord.IsAvailableToHost,
                IsVisibleToClients = definitionRecord.IsVisibleToClients,
                DefaultValue = definitionRecord.DefaultValue,
                ValueType = definitionRecord.ValueType,
                AllowedProviders = definitionRecord.AllowedProviders?.Split(',').ToList(),
                Description = definitionRecord.Description,
                DisplayName = definitionRecord.DisplayName,
                ExtraProperties = new ExtraPropertyDictionary(),
            };

            foreach (var property in definitionRecord.ExtraProperties)
            {
                dto.SetProperty(property.Key, property.Value);
            }

            return dto;
        }

        protected virtual FeatureDefinitionDto DefinitionToDto(FeatureGroupDefinition groupDefinition, FeatureDefinition definition, bool isStatic = false)
        {
            var dto = new FeatureDefinitionDto
            {
                IsStatic = isStatic,
                Name = definition.Name,
                GroupName = groupDefinition.Name,
                ParentName = definition.Parent?.Name,
                DefaultValue = definition.DefaultValue,
                AllowedProviders = definition.AllowedProviders,
                IsAvailableToHost = definition.IsAvailableToHost,
                IsVisibleToClients = definition.IsVisibleToClients,
                ValueType = definition.ValueType.Name,
                DisplayName = localizableStringSerializer.Serialize(definition.DisplayName),
                ExtraProperties = new ExtraPropertyDictionary(),
            };

            if (definition.ValueType != null)
            {
                dto.ValueType = stringValueTypeSerializer.Serialize(definition.ValueType);
            }

            if (definition.Description != null)
            {
                dto.Description = localizableStringSerializer.Serialize(definition.Description);
            }

            foreach (var property in definition.Properties)
            {
                dto.SetProperty(property.Key, property.Value);
            }

            return dto;
        }

    }
}
