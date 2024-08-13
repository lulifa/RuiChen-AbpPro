using OpenIddict.Abstractions;
using System.Linq.Dynamic.Core;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.OpenIddict;
using Volo.Abp.OpenIddict.Tokens;

namespace RuiChen.AbpPro.OpenIddict
{
    public class OpenIddictTokenAppService : OpenIddictApplicationServiceBase, IOpenIddictTokenAppService
    {
        private readonly IOpenIddictTokenManager tokenManager;
        private readonly IRepository<OpenIddictToken, Guid> tokenRepository;
        private readonly AbpOpenIddictIdentifierConverter identifierConverter;

        public OpenIddictTokenAppService(IOpenIddictTokenManager tokenManager, IRepository<OpenIddictToken, Guid> tokenRepository, AbpOpenIddictIdentifierConverter identifierConverter)
        {
            this.tokenManager = tokenManager;
            this.tokenRepository = tokenRepository;
            this.identifierConverter = identifierConverter;
        }

        public async virtual Task DeleteAsync(Guid id)
        {
            var token = await tokenManager.FindByIdAsync(identifierConverter.ToString(id));

            await tokenManager.DeleteAsync(token);

        }

        public async virtual Task<OpenIddictTokenDto> GetAsync(Guid id)
        {
            var token = await tokenRepository.GetAsync(id);

            return token.ToDto();
        }

        public async virtual Task<PagedResultDto<OpenIddictTokenDto>> GetListAsync(OpenIddictTokenGetListInput input)
        {
            var queryable = await tokenRepository.GetQueryableAsync();

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

            if (input.BeginExpirationDate.HasValue)
            {
                queryable = queryable.Where(item => item.ExpirationDate >= input.BeginExpirationDate);
            }

            if (input.EndExpirationDate.HasValue)
            {
                queryable = queryable.Where(item => item.ExpirationDate < input.EndExpirationDate);
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

            if (!input.ReferenceId.IsNullOrWhiteSpace())
            {
                queryable = queryable.Where(item => item.ReferenceId == input.ReferenceId);
            }

            if (!input.Filter.IsNullOrWhiteSpace())
            {
                queryable = queryable.Where(item => item.Subject.Contains(input.Filter) ||
                                                item.Status.Contains(input.Filter) ||
                                                item.Type.Contains(input.Filter) ||
                                                item.Payload.Contains(input.Filter) ||
                                                item.Properties.Contains(input.Filter) ||
                                                item.ReferenceId.Contains(input.Filter));
            }

            var totalCount = await AsyncExecuter.CountAsync(queryable);

            var sorting = input.Sorting;

            if (sorting.IsNullOrWhiteSpace())
            {
                sorting = $"{nameof(OpenIddictToken.CreationTime)} DESC";
            }

            queryable = queryable.OrderBy(sorting).PageBy(input.SkipCount, input.MaxResultCount);

            var entities = await AsyncExecuter.ToListAsync(queryable);

            var items = entities.Select(item => item.ToDto()).ToList();

            return new PagedResultDto<OpenIddictTokenDto>(totalCount, items);

        }
    }
}
