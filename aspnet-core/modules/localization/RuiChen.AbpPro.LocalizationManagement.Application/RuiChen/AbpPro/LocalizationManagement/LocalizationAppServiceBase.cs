using Volo.Abp.Application.Services;

namespace RuiChen.AbpPro.LocalizationManagement
{
    public abstract class LocalizationAppServiceBase : ApplicationService
    {
        protected LocalizationAppServiceBase()
        {
            LocalizationResource = typeof(LocalizationManagementResource);
            ObjectMapperContext = typeof(AbpProLocalizationManagementApplicationModule);
        }

    }
}
