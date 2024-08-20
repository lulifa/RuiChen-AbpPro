using Volo.Abp.Modularity;
using VoloAbpFeatureManagementApplicationModule = Volo.Abp.FeatureManagement.AbpFeatureManagementApplicationModule;

namespace RuiChen.AbpPro.FeatureManagement
{
    [DependsOn(
        typeof(VoloAbpFeatureManagementApplicationModule),
        typeof(AbpProFeatureManagementApplicationContractsModule)
        )]
    public class AbpProFeatureManagementApplicationModule : AbpModule
    {
    }
}
