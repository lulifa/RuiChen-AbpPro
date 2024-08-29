using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.OpenIddict.Localization;

namespace RuiChen.AbpPro.OpenIddict
{
    [Controller]
    [Authorize]
    [RemoteService(Name = OpenIddictRemoteServiceConsts.RemoteServiceName)]
    [Area(OpenIddictRemoteServiceConsts.ModuleName)]
    public abstract class OpenIddictControllerBase : AbpControllerBase
    {
        protected OpenIddictControllerBase()
        {
            LocalizationResource = typeof(AbpOpenIddictResource);
        }
    }
}
