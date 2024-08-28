namespace RuiChen.AbpPro.UI.Navigation
{
    public interface INavigationProvider
    {
        Task<IReadOnlyCollection<ApplicationMenu>> GetAllAsync();
    }
}
