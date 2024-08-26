using Volo.Abp.Application.Services;
using Volo.Abp.AuditLogging.Localization;

namespace RuiChen.AbpPro.Auditing
{
    public abstract class AuditingApplicationServiceBase : ApplicationService
    {
        protected AuditingApplicationServiceBase()
        {
            LocalizationResource = typeof(AuditLoggingResource);
            ObjectMapperContext = typeof(AbpProAuditingApplicationModule);
        }
    }
}
