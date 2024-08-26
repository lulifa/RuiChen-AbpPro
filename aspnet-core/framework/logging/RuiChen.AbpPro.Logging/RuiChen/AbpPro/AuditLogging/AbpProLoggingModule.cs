using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace RuiChen.AbpPro.Logging
{
    public class AbpProLoggingModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            Configure<AbpLoggingEnricherPropertyNames>(configuration.GetSection("Logging"));
        }
    }
}
