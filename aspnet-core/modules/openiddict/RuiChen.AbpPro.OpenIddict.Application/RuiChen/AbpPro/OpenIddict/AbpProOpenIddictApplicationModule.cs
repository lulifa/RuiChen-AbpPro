using RuiChen.AbpPro.OpenIddict.AspNetCore;
using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict;

namespace RuiChen.AbpPro.OpenIddict
{
    [DependsOn(
        typeof(AbpOpenIddictDomainModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpProOpenIddictApplicationContractsModule),
        typeof(AbpProOpenIddictAspNetCoreModule)
        )]
    public class AbpProOpenIddictApplicationModule : AbpModule
    {
    }
}
