using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;

namespace RuiChen.AbpPro.MultiTenancy
{
    [DependsOn(typeof(AbpMultiTenancyModule))]
    public class AbpProMultiTenancyEditionsModule : AbpModule
    {
    }
}
