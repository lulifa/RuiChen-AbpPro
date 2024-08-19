using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.Account.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace RuiChen.AbpPro.Account
{
    [Controller]
    [Authorize]
    [RemoteService(Name = AccountRemoteServiceConsts.RemoteServiceName)]
    [Area(AccountRemoteServiceConsts.ModuleName)]
    public abstract class AccountControllerBase : AbpControllerBase
    {
        protected AccountControllerBase()
        {
            LocalizationResource = typeof(AccountResource);
        }
    }
}
