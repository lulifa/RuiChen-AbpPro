using Volo.Abp.Application.Services;

namespace RuiChen.AbpPro.OpenIddict
{
    public interface IOpenIddictTokenAppService :
        IReadOnlyAppService<
            OpenIddictTokenDto,
            Guid,
            OpenIddictTokenGetListInput>,
        IDeleteAppService<Guid>
    {
    }
}
