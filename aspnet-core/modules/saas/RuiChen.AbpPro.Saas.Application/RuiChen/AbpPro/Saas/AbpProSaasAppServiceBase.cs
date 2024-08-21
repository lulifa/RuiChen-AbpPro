using RuiChen.AbpPro.Saas.Localization;
using Volo.Abp.Application.Services;

namespace RuiChen.AbpPro.Saas
{
    public abstract class AbpProSaasAppServiceBase : ApplicationService
    {
        protected AbpProSaasAppServiceBase()
        {
            ObjectMapperContext = typeof(AbpProSaasApplicationModule);
            LocalizationResource = typeof(AbpProSaasResource);
        }
    }
}
