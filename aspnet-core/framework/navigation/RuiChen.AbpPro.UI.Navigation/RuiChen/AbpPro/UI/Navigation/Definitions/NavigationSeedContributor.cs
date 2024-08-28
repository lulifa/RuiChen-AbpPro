using Volo.Abp.DependencyInjection;

namespace RuiChen.AbpPro.UI.Navigation
{
    public abstract class NavigationSeedContributor : INavigationSeedContributor, ITransientDependency
    {
        public abstract Task SeedAsync(NavigationSeedContext context);
    }
}
