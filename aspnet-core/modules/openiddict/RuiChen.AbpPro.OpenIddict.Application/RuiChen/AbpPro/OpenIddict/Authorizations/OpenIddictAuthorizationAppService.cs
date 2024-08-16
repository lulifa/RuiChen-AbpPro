using OpenIddict.Abstractions;
using System.Linq.Dynamic.Core;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.OpenIddict;
using Volo.Abp.OpenIddict.Authorizations;

namespace RuiChen.AbpPro.OpenIddict
{
    /// <summary>
    /// Openiddict授权管理
    /// </summary>
    public class OpenIddictAuthorizationAppService : OpenIddictApplicationServiceBase, IOpenIddictAuthorizationAppService
    {
        private readonly IOpenIddictAuthorizationManager authorizationManager;
        private readonly IRepository<OpenIddictAuthorization, Guid> authorizationRepository;
        private readonly AbpOpenIddictIdentifierConverter identifierConverter;

        public OpenIddictAuthorizationAppService(IOpenIddictAuthorizationManager authorizationManager, IRepository<OpenIddictAuthorization, Guid> authorizationRepository, AbpOpenIddictIdentifierConverter identifierConverter)
        {
            this.authorizationManager = authorizationManager;
            this.authorizationRepository = authorizationRepository;
            this.identifierConverter = identifierConverter;
        }

        /// <summary>
        /// 删除指定的OpenIddict授权
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async virtual Task DeleteAsync(Guid id)
        {
            var authorization = await authorizationManager.FindByIdAsync(identifierConverter.ToString(id));

            await authorizationManager.DeleteAsync(authorization);

        }

        /// <summary>
        /// 获取指定的OpenIddict授权
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async virtual Task<OpenIddictAuthorizationDto> GetAsync(Guid id)
        {
            var authorization = await authorizationRepository.GetAsync(id);

            return authorization.ToDto(JsonSerializer);

        }

        /// <summary>
        /// 分页获取OpenIddict授权
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async virtual Task<PagedResultDto<OpenIddictAuthorizationDto>> GetListAsync(OpenIddictAuthorizationGetListInput input)
        {
            var queryable = await authorizationRepository.GetQueryableAsync();

            if (input.ClientId.HasValue)
            {
                queryable = queryable.Where(item => item.ApplicationId == input.ClientId);
            }

            if (input.BeginCreationTime.HasValue)
            {
                queryable = queryable.Where(item => item.CreationTime >= input.BeginCreationTime);
            }

            if (input.EndCreationTime.HasValue)
            {
                queryable = queryable.Where(item => item.CreationTime <= input.EndCreationTime);
            }

            if (!input.Status.IsNullOrWhiteSpace())
            {
                queryable = queryable.Where(item => item.Status == input.Status);
            }

            if (!input.Type.IsNullOrWhiteSpace())
            {
                queryable = queryable.Where(item => item.Type == input.Type);
            }

            if (!input.Subject.IsNullOrWhiteSpace())
            {
                queryable = queryable.Where(item => item.Subject == input.Subject);
            }

            if (!input.Filter.IsNullOrWhiteSpace())
            {
                queryable = queryable.Where(item => item.Subject.Contains(input.Filter) ||
                                                    item.Status.Contains(input.Filter) ||
                                                    item.Type.Contains(input.Filter) ||
                                                    item.Scopes.Contains(input.Filter) ||
                                                    item.Properties.Contains(input.Filter));
            }

            var totalCount = await AsyncExecuter.CountAsync(queryable);

            var sorting = input.Sorting;

            if (sorting.IsNullOrWhiteSpace())
            {
                sorting = $"{nameof(OpenIddictAuthorization.CreationTime)} DESC";
            }

            queryable = queryable.OrderBy(sorting).PageBy(input.SkipCount, input.MaxResultCount);

            var entities = await AsyncExecuter.ToListAsync(queryable);

            var items = entities.Select(item => item.ToDto(JsonSerializer)).ToList();

            return new PagedResultDto<OpenIddictAuthorizationDto>(totalCount, items);

        }
    }
}
