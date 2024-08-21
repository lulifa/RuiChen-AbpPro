using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace RuiChen.AbpPro.SettingManagement
{
    [Controller]
    [Authorize]
    [RemoteService(Name = AbpSettingManagementRemoteServiceConsts.RemoteServiceName)]
    [Area(AbpSettingManagementRemoteServiceConsts.ModuleName)]
    public abstract class AbpProSettingControllerBase : AbpControllerBase
    {
    }
}
