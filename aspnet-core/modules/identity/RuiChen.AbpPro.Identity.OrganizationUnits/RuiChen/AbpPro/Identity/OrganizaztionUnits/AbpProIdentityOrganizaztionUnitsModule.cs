using RuiChen.AbpPro.Authorization;
using Volo.Abp.Modularity;

namespace RuiChen.AbpPro.Identity
{
    [DependsOn(
        typeof(AbpProIdentityDomainModule),
        typeof(AbpProAuthorizationOrganizationUnitsModule)
        )]
    public class AbpProIdentityOrganizaztionUnitsModule : AbpModule
    {
    }
}
