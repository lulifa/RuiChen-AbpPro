using Volo.Abp.Authorization;
using Volo.Abp.Modularity;
using VoloAbpIdentityApplicationContractsModule = Volo.Abp.Identity.AbpIdentityApplicationContractsModule;

namespace RuiChen.AbpPro.Identity
{
    [DependsOn(
        typeof(VoloAbpIdentityApplicationContractsModule),
        typeof(AbpProIdentityDomainSharedModule),
        typeof(AbpAuthorizationModule)
        )]
    public class AbpProIdentityApplicationContractsModule : AbpModule
    {
    }
}
