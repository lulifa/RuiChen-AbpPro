using JetBrains.Annotations;
using RuiChen.AbpPro.Identity;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Identity;

namespace Microsoft.AspNetCore.Identity
{
    public static class IdentityUserManagerExtensions
    {
        public static async Task<IdentityResult> SetTwoFactorEnabledWithAccountConfirmedAsync<TUser>([NotNull] this UserManager<TUser> userManager, [NotNull] TUser user, bool enabled) where TUser : IdentityUser
        {
            Check.NotNull(userManager, nameof(userManager));
            Check.NotNull(user, nameof(user));

            if (enabled)
            {
                var hasAuthenticatorEnabled = user.GetProperty(userManager.Options.Tokens.AuthenticatorTokenProvider, false);

                var phoneNumberConfirmed = await userManager.IsPhoneNumberConfirmedAsync(user);

                var emailAddressConfirmed = await userManager.IsEmailConfirmedAsync(user);

                if (!hasAuthenticatorEnabled && !phoneNumberConfirmed && !emailAddressConfirmed)
                {
                    throw new IdentityException(
                        RuiChen.AbpPro.Identity.IdentityErrorCodes.ChangeTwoFactorWithMFANotBound,
                        details: phoneNumberConfirmed ? "phone number not confirmed" : "email address not confirmed");
                }

            }

            return await userManager.SetTwoFactorEnabledAsync(user, enabled);
        }
    }
}
