using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace RuiChen.AbpPro.CachingManagement
{
    [Controller]
    [Authorize]
    [RemoteService(Name = CachingManagementRemoteServiceConsts.RemoteServiceName)]
    [Area(CachingManagementRemoteServiceConsts.ModuleName)]
    public abstract class CacheControllerBase : AbpControllerBase
    {
        protected CacheControllerBase()
        {
            LocalizationResource = typeof(CacheResource);
        }
    }
}
