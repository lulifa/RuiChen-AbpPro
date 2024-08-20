using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.PermissionManagement;
using Volo.Abp.PermissionManagement.Localization;

namespace RuiChen.AbpPro.PermissionManagement
{
    [Controller]
    [Authorize]
    [RemoteService(Name = PermissionManagementRemoteServiceConsts.RemoteServiceName)]
    [Area(PermissionManagementRemoteServiceConsts.ModuleName)]
    public abstract class PermissionManagementControllerBase : AbpControllerBase
    {
        protected PermissionManagementControllerBase()
        {
            LocalizationResource = typeof(AbpPermissionManagementResource);
        }
    }
}
