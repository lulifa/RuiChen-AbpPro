using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.ObjectExtending;

namespace RuiChen.AbpPro.UI.Navigation
{
    [DependsOn(
        typeof(AbpMultiTenancyModule),
        typeof(AbpObjectExtendingModule)
        )]
    public class AbpUINavigationModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            AutoAddDefinitionProviders(context.Services);
        }

        private static void AutoAddDefinitionProviders(IServiceCollection services)
        {
            var definitionProviders = new List<Type>();

            services.OnRegistered(context =>
            {
                if (typeof(INavigationDefinitionProvider).IsAssignableFrom(context.ImplementationType))
                {
                    definitionProviders.Add(context.ImplementationType);
                }
            });

            services.Configure<AbpNavigationOptions>(options =>
            {
                options.DefinitionProviders.AddIfNotContains(definitionProviders);
            });
        }
    }
}
