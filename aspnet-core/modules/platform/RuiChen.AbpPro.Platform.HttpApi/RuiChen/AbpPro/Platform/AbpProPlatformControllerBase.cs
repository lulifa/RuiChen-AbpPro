using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Settings;

namespace RuiChen.AbpPro.Platform
{
    [Controller]
    [Authorize]
    [RemoteService(Name = AbpProPlatformRemoteServiceConsts.RemoteServiceName)]
    [Area(AbpProPlatformRemoteServiceConsts.ModuleName)]
    public abstract class AbpProPlatformControllerBase : AbpControllerBase
    {
        protected ISettingProvider SettingProvider => LazyServiceProvider.LazyGetRequiredService<ISettingProvider>();

        protected AbpProPlatformControllerBase()
        {
            LocalizationResource = typeof(PlatformResource);
        }
    }
}
