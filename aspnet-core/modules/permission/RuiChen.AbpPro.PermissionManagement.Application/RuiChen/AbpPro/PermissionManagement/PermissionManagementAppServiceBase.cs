using Volo.Abp.Application.Services;
using Volo.Abp.PermissionManagement;
using Volo.Abp.PermissionManagement.Localization;

namespace RuiChen.AbpPro.PermissionManagement
{
    public abstract class PermissionManagementAppServiceBase : ApplicationService
    {
        protected PermissionManagementAppServiceBase()
        {
            LocalizationResource = typeof(AbpPermissionManagementResource);
            ObjectMapperContext = typeof(AbpPermissionManagementApplicationModule);
        }
    }
}
