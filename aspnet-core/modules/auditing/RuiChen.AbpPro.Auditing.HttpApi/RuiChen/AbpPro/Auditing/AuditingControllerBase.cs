using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AuditLogging.Localization;

namespace RuiChen.AbpPro.Auditing
{
    [Controller]
    [Authorize]
    [RemoteService(Name = AuditingRemoteServiceConsts.RemoteServiceName)]
    [Area(AuditingRemoteServiceConsts.ModuleName)]
    public abstract class AuditingControllerBase : AbpControllerBase
    {
        protected AuditingControllerBase()
        {
            LocalizationResource = typeof(AuditLoggingResource);
        }
    }
}
