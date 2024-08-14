using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace RuiChen.AbpPro.Identity
{
    [Authorize(IdentityPermissions.IdentityClaimType.Default)]
    public class IdentityClaimTypeAppService : IdentityAppServiceBase, IIdentityClaimTypeAppService
    {
        private readonly IdentityClaimTypeManager identityClaimTypeManager;
        private readonly IIdentityClaimTypeRepository identityClaimTypeRepository;

        public IdentityClaimTypeAppService(IdentityClaimTypeManager identityClaimTypeManager, IIdentityClaimTypeRepository identityClaimTypeRepository)
        {
            this.identityClaimTypeManager = identityClaimTypeManager;
            this.identityClaimTypeRepository = identityClaimTypeRepository;
        }

        public async virtual Task<IdentityClaimTypeDto> CreateAsync(IdentityClaimTypeCreateDto input)
        {
            if (await identityClaimTypeRepository.AnyAsync(input.Name))
            {
                throw new UserFriendlyException(L["IdentityClaimTypeAlreadyExists", input.Name]);
            }

            var identityClaimType = new IdentityClaimType(
                GuidGenerator.Create(),
                input.Name,
                input.Required,
                input.IsStatic,
                input.Regex,
                input.RegexDescription,
                input.Description,
                input.ValueType
            );

            identityClaimType = await identityClaimTypeManager.CreateAsync(identityClaimType);

            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<IdentityClaimType, IdentityClaimTypeDto>(identityClaimType);
        }

        public async virtual Task DeleteAsync(Guid id)
        {
            var identityClaimType = await identityClaimTypeRepository.FindAsync(id);

            if (identityClaimType == null)
            {
                return;
            }

            CheckDeletionClaimType(identityClaimType);

            await identityClaimTypeRepository.DeleteAsync(identityClaimType);
        }

        public async virtual Task<ListResultDto<IdentityClaimTypeDto>> GetAllListAsync()
        {
            var identityClaimTypes = await identityClaimTypeRepository.GetListAsync();

            var items = ObjectMapper.Map<List<IdentityClaimType>, List<IdentityClaimTypeDto>>(identityClaimTypes);

            return new ListResultDto<IdentityClaimTypeDto>(items);
        }

        public async virtual Task<IdentityClaimTypeDto> GetAsync(Guid id)
        {
            var identityClaimType = await identityClaimTypeRepository.FindAsync(id);

            return ObjectMapper.Map<IdentityClaimType, IdentityClaimTypeDto>(identityClaimType);
        }

        public async virtual Task<PagedResultDto<IdentityClaimTypeDto>> GetListAsync(IdentityClaimTypeGetByPagedDto input)
        {
            var identityClaimTypeCount = await identityClaimTypeRepository.GetCountAsync(input.Filter);

            var identityClaimTypes = await identityClaimTypeRepository.GetListAsync(input.Sorting, input.MaxResultCount, input.SkipCount, input.Filter);

            var items = ObjectMapper.Map<List<IdentityClaimType>, List<IdentityClaimTypeDto>>(identityClaimTypes);

            return new PagedResultDto<IdentityClaimTypeDto>(identityClaimTypeCount, items);
        }

        public async virtual Task<IdentityClaimTypeDto> UpdateAsync(Guid id, IdentityClaimTypeUpdateDto input)
        {
            var identityClaimType = await identityClaimTypeRepository.GetAsync(id);

            CheckChangingClaimType(identityClaimType);

            identityClaimType.Required = input.Required;

            if (!string.Equals(identityClaimType.Regex, input.Regex, StringComparison.InvariantCultureIgnoreCase))
            {
                identityClaimType.Regex = input.Regex;
            }

            if (!string.Equals(identityClaimType.RegexDescription, input.RegexDescription, StringComparison.InvariantCultureIgnoreCase))
            {
                identityClaimType.RegexDescription = input.RegexDescription;
            }

            if (!string.Equals(identityClaimType.Description, input.Description, StringComparison.InvariantCultureIgnoreCase))
            {
                identityClaimType.Description = input.Description;
            }

            identityClaimType = await identityClaimTypeManager.UpdateAsync(identityClaimType);

            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<IdentityClaimType, IdentityClaimTypeDto>(identityClaimType);
        }

        protected virtual void CheckChangingClaimType(IdentityClaimType claimType)
        {
            if (claimType.IsStatic)
            {
                throw new BusinessException(IdentityErrorCodes.StaticClaimTypeChange);
            }
        }

        protected virtual void CheckDeletionClaimType(IdentityClaimType claimType)
        {
            if (claimType.IsStatic)
            {
                throw new BusinessException(IdentityErrorCodes.StaticClaimTypeDeletion);
            }
        }

    }
}
