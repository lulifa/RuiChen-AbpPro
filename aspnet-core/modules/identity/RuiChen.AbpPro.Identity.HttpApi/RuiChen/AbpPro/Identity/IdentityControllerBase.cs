using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Identity;
using Volo.Abp.Identity.Localization;

namespace RuiChen.AbpPro.Identity
{
    [Controller]
    [Authorize]
    [RemoteService(Name = IdentityRemoteServiceConsts.RemoteServiceName)]
    [Area(IdentityRemoteServiceConsts.ModuleName)]
    public abstract class IdentityControllerBase : AbpControllerBase
    {
        protected IdentityControllerBase()
        {
            LocalizationResource = typeof(IdentityResource);
        }
    }
}
