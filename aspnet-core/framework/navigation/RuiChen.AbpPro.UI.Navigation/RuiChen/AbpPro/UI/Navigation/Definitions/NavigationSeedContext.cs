using Volo.Abp.MultiTenancy;

namespace RuiChen.AbpPro.UI.Navigation
{
    public class NavigationSeedContext
    {
        public IReadOnlyCollection<ApplicationMenu> Menus { get; }

        public MultiTenancySides MultiTenancySides { get; }

        public NavigationSeedContext(
            IReadOnlyCollection<ApplicationMenu> menus,
            MultiTenancySides multiTenancySides = MultiTenancySides.Both)
        {
            Menus = menus;
            MultiTenancySides = multiTenancySides;
        }
    }
}
