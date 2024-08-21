using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace RuiChen.AbpPro.Saas
{
    [DependsOn(
        typeof(AbpAuthorizationAbstractionsModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpProSaasDomainSharedModule)
        )]
    public class AbpProSaasApplicationContractsModule : AbpModule
    {
    }
}
