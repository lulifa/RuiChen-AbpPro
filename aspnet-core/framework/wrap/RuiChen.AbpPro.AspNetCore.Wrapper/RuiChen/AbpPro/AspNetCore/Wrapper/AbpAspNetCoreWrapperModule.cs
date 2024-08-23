using RuiChen.AbpPro.Wrapper;
using Volo.Abp.AspNetCore;
using Volo.Abp.Modularity;

namespace RuiChen.AbpPro.AspNetCore.Wrapper
{
    [DependsOn(
        typeof(AbpAspNetCoreModule),
        typeof(AbpWrapperModule)
        )]
    public class AbpAspNetCoreWrapperModule : AbpModule
    {
    }
}
