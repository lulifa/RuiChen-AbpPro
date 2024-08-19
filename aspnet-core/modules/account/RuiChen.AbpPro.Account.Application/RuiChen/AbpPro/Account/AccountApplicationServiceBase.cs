using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using RuiChen.AbpPro.Identity;
using Volo.Abp.Account.Localization;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;
using Volo.Abp.Users;

namespace RuiChen.AbpPro.Account
{
    public class AccountApplicationServiceBase : ApplicationService
    {
        protected IOptions<IdentityOptions> IdentityOptions => LazyServiceProvider.LazyGetRequiredService<IOptions<IdentityOptions>>();
        protected IdentityUserStore UserStore => LazyServiceProvider.LazyGetRequiredService<IdentityUserStore>();
        protected IdentityUserManager UserManager => LazyServiceProvider.LazyGetRequiredService<IdentityUserManager>();
        protected IAccountEmailVerifySender AccountEmailVerifySender => LazyServiceProvider.LazyGetRequiredService<IAccountEmailVerifySender>();
        protected IAccountEmailConfirmSender AccountEmailConfirmSender => LazyServiceProvider.LazyGetRequiredService<IAccountEmailConfirmSender>();
        protected IAuthenticatorUriGenerator AuthenticatorUriGenerator => LazyServiceProvider.LazyGetRequiredService<IAuthenticatorUriGenerator>();

        protected AccountApplicationServiceBase()
        {
            LocalizationResource = typeof(AccountResource);
        }

        protected async virtual Task<IdentityUser> GetCurrentUserAsync()
        {
            await IdentityOptions.SetAsync();

            return await UserManager.GetByIdAsync(CurrentUser.GetId());
        }
    }
}
