namespace RuiChen.AbpPro.UI.Navigation
{
    public interface INavigationDefinitionManager
    {
        IReadOnlyList<NavigationDefinition> GetAll();
    }
}
