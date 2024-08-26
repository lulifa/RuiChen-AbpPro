using Volo.Abp.Modularity;

using VoloAbpExceptionHandlingModule = Volo.Abp.ExceptionHandling.AbpExceptionHandlingModule;

namespace RuiChen.AbpPro.ExceptionHandling
{
    [DependsOn(typeof(VoloAbpExceptionHandlingModule))]
    public class AbpProExceptionHandlingModule : AbpModule
    {
    }
}
