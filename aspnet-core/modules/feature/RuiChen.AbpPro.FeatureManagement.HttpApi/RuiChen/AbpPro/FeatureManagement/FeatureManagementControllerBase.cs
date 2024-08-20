using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.FeatureManagement;
using Volo.Abp.FeatureManagement.Localization;

namespace RuiChen.AbpPro.FeatureManagement
{
    [Controller]
    [Authorize]
    [RemoteService(Name = FeatureManagementRemoteServiceConsts.RemoteServiceName)]
    [Area(FeatureManagementRemoteServiceConsts.ModuleName)]
    public abstract class FeatureManagementControllerBase : AbpControllerBase
    {
        protected FeatureManagementControllerBase()
        {
            LocalizationResource = typeof(AbpFeatureManagementResource);
        }
    }
}
