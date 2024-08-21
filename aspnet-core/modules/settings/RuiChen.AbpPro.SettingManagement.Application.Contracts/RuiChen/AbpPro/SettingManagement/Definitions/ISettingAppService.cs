namespace RuiChen.AbpPro.SettingManagement
{
    public interface ISettingAppService : IReadonlySettingAppService
    {
        Task SetGlobalAsync(UpdateSettingsDto input);

        Task SetCurrentTenantAsync(UpdateSettingsDto input);
    }
}
