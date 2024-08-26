using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace RuiChen.AbpPro.CachingManagement
{
    [DependsOn(
        typeof(AbpDddApplicationModule),
        typeof(AbpProCachingManagementApplicationContractsModule)
        )]
    public class AbpProCachingManagementApplicationModule:AbpModule
    {
    }
}
