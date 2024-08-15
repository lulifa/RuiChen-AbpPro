using System.Text.Encodings.Web;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace RuiChen.AbpPro.Identity
{
    /// <summary>
    /// DefaultAuthenticatorUriGenerator 类的主要作用是生成符合 TOTP 规范的认证 URI，供双因素认证系统使用。
    /// 它在用户配置双因素认证或生成二维码时被调用。通过这个 URI，用户可以在 OTP 应用中设置他们的账户，从而实现双因素认证
    /// </summary>
    public class DefaultAuthenticatorUriGenerator : IAuthenticatorUriGenerator, ITransientDependency
    {
        protected const string OTatpUrlFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";
        private readonly UrlEncoder urlEncoder;
        private readonly IApplicationInfoAccessor applicationInfoAccessor;

        public DefaultAuthenticatorUriGenerator(UrlEncoder urlEncoder, IApplicationInfoAccessor applicationInfoAccessor)
        {
            this.urlEncoder = urlEncoder;
            this.applicationInfoAccessor = applicationInfoAccessor;
        }

        public virtual string Generate(string email, string unformattedKey)
        {
            var application = urlEncoder.Encode(applicationInfoAccessor.ApplicationName);
            var account = urlEncoder.Encode(email);

            return string.Format(OTatpUrlFormat, application, account, unformattedKey, application);
        }
    }
}
