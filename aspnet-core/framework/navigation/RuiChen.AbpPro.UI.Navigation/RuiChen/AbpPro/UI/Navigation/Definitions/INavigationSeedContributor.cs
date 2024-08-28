namespace RuiChen.AbpPro.UI.Navigation
{
    public interface INavigationSeedContributor
    {
        Task SeedAsync(NavigationSeedContext context);
    }
}
