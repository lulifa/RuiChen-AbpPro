using Microsoft.AspNetCore.Mvc;

namespace RuiChen.AbpPro.SettingManagement
{
    /// <summary>
    /// 用户设置
    /// </summary>
    [Route("api/setting-management/settings")]
    public class UserSettingController : AbpProSettingControllerBase, IUserSettingAppService
    {
        private readonly IUserSettingAppService _service;

        public UserSettingController(
            IUserSettingAppService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("by-current-user")]
        public async virtual Task<SettingGroupResult> GetAllForCurrentUserAsync()
        {
            return await _service.GetAllForCurrentUserAsync();
        }

        [HttpPut]
        [Route("change-current-user")]
        public async virtual Task SetCurrentUserAsync(UpdateSettingsDto input)
        {
            await _service.SetCurrentUserAsync(input);
        }
    }
}
