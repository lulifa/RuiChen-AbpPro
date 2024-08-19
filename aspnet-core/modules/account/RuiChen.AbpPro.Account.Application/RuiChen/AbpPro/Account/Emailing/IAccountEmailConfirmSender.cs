using Volo.Abp.Identity;

namespace RuiChen.AbpPro.Account
{
    public interface IAccountEmailConfirmSender
    {

        Task SendEmailConfirmLinkAsync(
        IdentityUser user,
        string confirmToken,
        string appName,
        string returnUrl = null,
        string returnUrlHash = null
    );

    }
}
