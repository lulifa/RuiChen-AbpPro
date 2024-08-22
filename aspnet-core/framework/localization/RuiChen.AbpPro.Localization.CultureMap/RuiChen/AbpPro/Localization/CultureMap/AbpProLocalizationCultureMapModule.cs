using Volo.Abp.AspNetCore;
using Volo.Abp.Modularity;

namespace RuiChen.AbpPro.Localization
{
    [DependsOn(
        typeof(AbpAspNetCoreModule)
        )]
    public class AbpProLocalizationCultureMapModule : AbpModule
    {
    }
}
