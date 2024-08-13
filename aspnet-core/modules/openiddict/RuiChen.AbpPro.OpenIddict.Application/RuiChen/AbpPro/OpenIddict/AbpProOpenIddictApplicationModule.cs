using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict;

namespace RuiChen.AbpPro.OpenIddict
{
    [DependsOn(
        typeof(AbpProOpenIddictApplicationContractsModule),
        typeof(AbpOpenIddictDomainModule),
        typeof(AbpDddApplicationModule)
        )]
    public class AbpProOpenIddictApplicationModule : AbpModule
    {
    }
}
