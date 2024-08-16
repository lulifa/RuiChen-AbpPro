using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace RuiChen.AbpPro.Identity
{
    /// <summary>
    /// 身份声明类型应用服务
    /// </summary>
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

        /// <summary>
        /// 创建新的身份声明类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
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

        /// <summary>
        ///  删除指定的身份声明类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        ///  获取所有身份声明类型的列表
        /// </summary>
        /// <returns></returns>
        public async virtual Task<ListResultDto<IdentityClaimTypeDto>> GetAllListAsync()
        {
            var identityClaimTypes = await identityClaimTypeRepository.GetListAsync();

            var items = ObjectMapper.Map<List<IdentityClaimType>, List<IdentityClaimTypeDto>>(identityClaimTypes);

            return new ListResultDto<IdentityClaimTypeDto>(items);
        }

        /// <summary>
        /// 根据唯一标识符获取身份声明类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async virtual Task<IdentityClaimTypeDto> GetAsync(Guid id)
        {
            var identityClaimType = await identityClaimTypeRepository.FindAsync(id);

            return ObjectMapper.Map<IdentityClaimType, IdentityClaimTypeDto>(identityClaimType);
        }

        /// <summary>
        /// 分页获取身份声明类型列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async virtual Task<PagedResultDto<IdentityClaimTypeDto>> GetListAsync(IdentityClaimTypeGetByPagedDto input)
        {
            var identityClaimTypeCount = await identityClaimTypeRepository.GetCountAsync(input.Filter);

            var identityClaimTypes = await identityClaimTypeRepository.GetListAsync(input.Sorting, input.MaxResultCount, input.SkipCount, input.Filter);

            var items = ObjectMapper.Map<List<IdentityClaimType>, List<IdentityClaimTypeDto>>(identityClaimTypes);

            return new PagedResultDto<IdentityClaimTypeDto>(identityClaimTypeCount, items);
        }

        /// <summary>
        /// 更新指定的身份声明类型
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 检查是否允许更改指定的声明类型
        /// </summary>
        /// <param name="claimType"></param>
        /// <exception cref="BusinessException"></exception>
        protected virtual void CheckChangingClaimType(IdentityClaimType claimType)
        {
            if (claimType.IsStatic)
            {
                throw new BusinessException(IdentityErrorCodes.StaticClaimTypeChange);
            }
        }

        /// <summary>
        /// 检查是否允许删除指定的声明类型
        /// </summary>
        /// <param name="claimType"></param>
        /// <exception cref="BusinessException"></exception>
        protected virtual void CheckDeletionClaimType(IdentityClaimType claimType)
        {
            if (claimType.IsStatic)
            {
                throw new BusinessException(IdentityErrorCodes.StaticClaimTypeDeletion);
            }
        }

    }
}
