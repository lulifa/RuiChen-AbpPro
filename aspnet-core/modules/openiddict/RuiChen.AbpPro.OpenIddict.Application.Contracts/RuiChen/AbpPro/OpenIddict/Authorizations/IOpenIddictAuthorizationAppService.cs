using Volo.Abp.Application.Services;

namespace RuiChen.AbpPro.OpenIddict
{
    public interface IOpenIddictAuthorizationAppService :
        IReadOnlyAppService<
            OpenIddictAuthorizationDto,
            Guid,
            OpenIddictAuthorizationGetListInput>,
        IDeleteAppService<Guid>
    {
    }
}
