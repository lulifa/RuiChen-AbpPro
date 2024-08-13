using Volo.Abp.Application.Services;

namespace RuiChen.AbpPro.OpenIddict
{
    public interface IOpenIddictApplicationAppService :
        ICrudAppService<
            OpenIddictApplicationDto,
            Guid,
            OpenIddictApplicationGetListInput,
            OpenIddictApplicationCreateDto,
            OpenIddictApplicationUpdateDto>
    {
    }
}
