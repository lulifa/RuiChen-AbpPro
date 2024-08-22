using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization.Resources.AbpLocalization;

namespace RuiChen.AbpPro.LocalizationManagement
{
    [Controller]
    [Authorize]
    [RemoteService(Name = LocalizationRemoteServiceConsts.RemoteServiceName)]
    [Area(LocalizationRemoteServiceConsts.ModuleName)]
    public abstract class LocalizationControllerBase : AbpControllerBase
    {
        protected LocalizationControllerBase()
        {
            LocalizationResource = typeof(AbpLocalizationResource);
        }
    }
}
