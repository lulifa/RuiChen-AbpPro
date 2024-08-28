using Volo.Abp.DependencyInjection;

namespace RuiChen.AbpPro.Platform.Navigation.VuePureAdmin
{
    [Dependency(ReplaceServices = true)]
    public class VuePureAdminStandardMenuConverter : IStandardMenuConverter, ISingletonDependency
    {
        public StandardMenu Convert(Menu menu)
        {
            var standardMenu = new StandardMenu
            {
                Icon = "",
                Name = menu.Name,
                Path = menu.Path,
                DisplayName = menu.DisplayName,
                Description = menu.Description,
                Redirect = menu.Redirect,
            };

            if (menu.ExtraProperties.TryGetValue("icon", out var icon))
            {
                standardMenu.Icon = icon.ToString();
            }

            return standardMenu;
        }
    }
}
