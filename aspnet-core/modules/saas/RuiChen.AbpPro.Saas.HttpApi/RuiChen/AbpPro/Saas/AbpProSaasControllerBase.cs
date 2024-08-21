using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RuiChen.AbpPro.Saas.Localization;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace RuiChen.AbpPro.Saas
{
    [Controller]
    [Authorize]
    [RemoteService(Name = AbpProSaasRemoteServiceConsts.RemoteServiceName)]
    [Area(AbpProSaasRemoteServiceConsts.ModuleName)]
    public abstract class AbpProSaasControllerBase : AbpControllerBase
    {
        protected AbpProSaasControllerBase()
        {
            LocalizationResource = typeof(AbpProSaasResource);
        }
    }
}
