using Volo.Abp.Account.Localization;
using Volo.Abp.Emailing.Templates;
using Volo.Abp.Localization;
using Volo.Abp.TextTemplating;

namespace RuiChen.AbpPro.Account
{
    public class AccountTemplateDefinitionProvider : TemplateDefinitionProvider
    {
        public override void Define(ITemplateDefinitionContext context)
        {
            TemplateDefinition[] templateDefinitions = [
                new TemplateDefinition(
                    name:AccountEmailTemplates.MailSecurityVerifyLink,
                    displayName:L($"TextTemplate:{AccountEmailTemplates.MailSecurityVerifyLink}"),
                    layout:StandardEmailTemplates.Layout,
                    localizationResource:typeof(AccountResource)
                    ).WithVirtualFilePath("/RuiChen/AbpPro/Account/Emailing/Templates/MailSecurityVerify.tpl",true),
                new TemplateDefinition(
                    name:AccountEmailTemplates.MailConfirmLink,
                    displayName:L($"TextTemplate:{AccountEmailTemplates.MailConfirmLink}"),
                    layout:StandardEmailTemplates.Layout,
                    localizationResource:typeof(AccountResource)
                    ).WithVirtualFilePath("/RuiChen/AbpPro/Account/Emailing/Templates/MailConfirm.tpl",true)
                ];

            context.Add(templateDefinitions);

        }

        private static ILocalizableString L(string name)
        {
            return LocalizableString.Create<AccountResource>(name);
        }
    }
}
