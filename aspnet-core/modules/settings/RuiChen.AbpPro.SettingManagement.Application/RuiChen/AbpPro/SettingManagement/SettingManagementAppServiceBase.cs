using Volo.Abp.Application.Services;
using Volo.Abp.SettingManagement.Localization;

namespace RuiChen.AbpPro.SettingManagement
{
    public abstract class SettingManagementAppServiceBase : ApplicationService
    {
        protected SettingManagementAppServiceBase()
        {
            ObjectMapperContext = typeof(AbpProSettingManagementApplicationModule);
            LocalizationResource = typeof(AbpSettingManagementResource);
        }
    }
}
