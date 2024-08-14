using System.Text.Encodings.Web;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace RuiChen.AbpPro.Identity
{
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
