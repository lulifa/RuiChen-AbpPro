using Microsoft.AspNetCore.Authorization;
using OpenIddict.Abstractions;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;
using Volo.Abp.OpenIddict;
using Volo.Abp.OpenIddict.Scopes;

namespace RuiChen.AbpPro.OpenIddict
{
    /// <summary>
    /// Openiddict作用域
    /// </summary>
    public class OpenIddictScopeAppService : OpenIddictApplicationServiceBase, IOpenIddictScopeAppService
    {
        private readonly IOpenIddictScopeManager scopeManager;
        private readonly IOpenIddictScopeRepository scopeRepository;
        private readonly AbpOpenIddictIdentifierConverter identifierConverter;

        public OpenIddictScopeAppService(IOpenIddictScopeManager scopeManager, IOpenIddictScopeRepository scopeRepository, AbpOpenIddictIdentifierConverter identifierConverter)
        {
            this.scopeManager = scopeManager;
            this.scopeRepository = scopeRepository;
            this.identifierConverter = identifierConverter;
        }

        /// <summary>
        /// 创建新的OpenIddict作用域
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(AbpProOpenIddictPermissions.Scopes.Create)]
        public async virtual Task<OpenIddictScopeDto> CreateAsync(OpenIddictScopeCreateDto input)
        {
            if (await scopeManager.FindByNameAsync(input.Name) != null)
            {
                throw new BusinessException(OpenIddictApplicationErrorCodes.Scopes.NameExisted)
                .WithData(nameof(OpenIddictScope.Name), input.Name);
            }

            var scope = new OpenIddictScope(GuidGenerator.Create());

            scope = input.ToEntity(scope, JsonSerializer);

            await scopeManager.CreateAsync(scope.ToModel());

            scope = await scopeRepository.FindByIdAsync(scope.Id);

            return scope.ToDto(JsonSerializer);

        }

        /// <summary>
        /// 删除指定的OpenIddict作用域
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(AbpProOpenIddictPermissions.Scopes.Delete)]
        public async virtual Task DeleteAsync(Guid id)
        {
            var scope = await scopeManager.FindByIdAsync(identifierConverter.ToString(id));

            await scopeManager.DeleteAsync(scope);

        }

        /// <summary>
        /// 更新指定的OpenIddict作用域
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(AbpProOpenIddictPermissions.Scopes.Update)]
        public async virtual Task<OpenIddictScopeDto> UpdateAsync(Guid id, OpenIddictScopeUpdateDto input)
        {
            var scope = await scopeRepository.GetAsync(id);

            if (!string.Equals(scope.Name, input.Name) && await scopeRepository.FindByNameAsync(input.Name) != null)
            {
                throw new BusinessException(OpenIddictApplicationErrorCodes.Scopes.NameExisted)
                .WithData(nameof(OpenIddictScope.Name), input.Name);
            }

            scope.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);

            scope = input.ToEntity(scope, JsonSerializer);

            await scopeManager.UpdateAsync(scope.ToModel());

            scope = await scopeRepository.GetAsync(id);

            return scope.ToDto(JsonSerializer);

        }

        /// <summary>
        /// 获取指定的OpenIddict作用域
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(AbpProOpenIddictPermissions.Scopes.Default)]
        public async virtual Task<OpenIddictScopeDto> GetAsync(Guid id)
        {
            var scope = await scopeRepository.GetAsync(id);

            return scope.ToDto(JsonSerializer);
        }

        /// <summary>
        /// 分页获取OpenIddict的作用域列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(AbpProOpenIddictPermissions.Scopes.Default)]
        public async virtual Task<PagedResultDto<OpenIddictScopeDto>> GetListAsync(OpenIddictScopeGetListInput input)
        {
            var totalCount = await scopeRepository.GetCountAsync(input.Filter);

            var entities = await scopeRepository.GetListAsync(input.Sorting, input.SkipCount, input.MaxResultCount, input.Filter);

            var items = entities.Select(item => item.ToDto(JsonSerializer)).ToList();

            return new PagedResultDto<OpenIddictScopeDto>(totalCount, items);

        }

        
    }
}
