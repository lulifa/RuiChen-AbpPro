using Volo.Abp.Application.Services;

namespace RuiChen.AbpPro.SettingManagement
{
    public interface IUserSettingAppService : IApplicationService
    {
        Task SetCurrentUserAsync(UpdateSettingsDto input);

        Task<SettingGroupResult> GetAllForCurrentUserAsync();
    }
}
