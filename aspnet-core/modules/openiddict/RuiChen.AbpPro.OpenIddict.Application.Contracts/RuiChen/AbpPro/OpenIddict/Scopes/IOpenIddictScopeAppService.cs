using Volo.Abp.Application.Services;

namespace RuiChen.AbpPro.OpenIddict
{
    public interface IOpenIddictScopeAppService :
        ICrudAppService<
            OpenIddictScopeDto,
            Guid,
            OpenIddictScopeGetListInput,
            OpenIddictScopeCreateDto,
            OpenIddictScopeUpdateDto>
    {
    }
}
