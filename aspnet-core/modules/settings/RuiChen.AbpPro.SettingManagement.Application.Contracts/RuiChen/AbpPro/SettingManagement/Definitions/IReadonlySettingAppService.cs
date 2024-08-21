using Volo.Abp.Application.Services;

namespace RuiChen.AbpPro.SettingManagement
{
    public interface IReadonlySettingAppService : IApplicationService
    {
        Task<SettingGroupResult> GetAllForGlobalAsync();

        Task<SettingGroupResult> GetAllForCurrentTenantAsync();
    }
}
