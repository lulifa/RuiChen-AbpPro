using RuiChen.AbpPro.UI.Navigation;
using Volo.Abp.Modularity;

namespace RuiChen.AbpPro.Platform.Navigation.VuePureAdmin
{
    [DependsOn(
        typeof(AbpUINavigationModule),
        typeof(AbpProPlatformDomainModule)
        )]
    public class AbpProNavigationVuePureAdminModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpNavigationOptions>(options =>
            {
                options.NavigationSeedContributors.Add<VuePureAdminNavigationSeedContributor>();
            });
        }
    }
}
