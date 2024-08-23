using Volo.Abp.ExceptionHandling;
using Volo.Abp.Modularity;

namespace RuiChen.AbpPro.Wrapper
{
    [DependsOn(
        typeof(AbpExceptionHandlingModule)
        )]
    public class AbpWrapperModule : AbpModule
    {
    }
}
