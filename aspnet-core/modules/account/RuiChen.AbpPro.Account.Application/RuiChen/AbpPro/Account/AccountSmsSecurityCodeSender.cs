using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Sms;

namespace RuiChen.AbpPro.Account
{
    public class AccountSmsSecurityCodeSender : IAccountSmsSecurityCodeSender, ITransientDependency
    {
        private readonly ISmsSender smsSender;

        public AccountSmsSecurityCodeSender(ISmsSender smsSender)
        {
            this.smsSender = smsSender;
        }

        public async Task SendSmsCodeAsync(string phone, string token, string template, CancellationToken cancellation = default)
        {
            Check.NotNullOrWhiteSpace(template, nameof(template));

            var smsMessage = new SmsMessage(phone, token);

            smsMessage.Properties.Add("code", token);

            smsMessage.Properties.Add("TemplateCode", template);

            await smsSender.SendAsync(smsMessage);

        }
    }
}
