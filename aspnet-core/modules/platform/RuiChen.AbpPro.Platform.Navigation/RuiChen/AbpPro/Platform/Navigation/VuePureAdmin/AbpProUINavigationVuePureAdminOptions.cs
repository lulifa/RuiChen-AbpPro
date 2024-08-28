namespace RuiChen.AbpPro.Platform.Navigation.VuePureAdmin
{
    public class AbpProUINavigationVuePureAdminOptions
    {
        public string UI { get; set; }
        public string LayoutName { get; set; }
        public string LayoutPath { get; set; }
        public AbpProUINavigationVuePureAdminOptions()
        {
            UI = "Vue Pure Admin";
            LayoutName = "Pure Admin Layout";
            LayoutPath = "LAYOUT";
        }
    }
}
