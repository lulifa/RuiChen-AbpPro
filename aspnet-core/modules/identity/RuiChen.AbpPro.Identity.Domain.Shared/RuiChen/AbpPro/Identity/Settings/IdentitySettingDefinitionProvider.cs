using Volo.Abp.Identity.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace RuiChen.AbpPro.Identity
{
    public class IdentitySettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            SettingDefinition[] settingDefinitions = [

                new SettingDefinition(
                    name: IdentitySettingNames.User.SmsNewUserRegister,
                    defaultValue: string.Empty,
                    displayName: L("DisplayName:Abp.Identity.User.SmsNewUserRegister"),
                    description: L("Description:Abp.Identity.User.SmsNewUserRegister"),
                    isVisibleToClients: true)
                .WithProviders(
                    DefaultValueSettingValueProvider.ProviderName,
                    ConfigurationSettingValueProvider.ProviderName,
                    GlobalSettingValueProvider.ProviderName,
                    TenantSettingValueProvider.ProviderName),

                new SettingDefinition(
                    name: IdentitySettingNames.User.SmsUserSignin,
                    defaultValue: string.Empty,
                    displayName: L("DisplayName:Abp.Identity.User.SmsUserSignin"),
                    description: L("Description:Abp.Identity.User.SmsUserSignin"),
                    isVisibleToClients: true)
                .WithProviders(
                    DefaultValueSettingValueProvider.ProviderName,
                    ConfigurationSettingValueProvider.ProviderName,
                    GlobalSettingValueProvider.ProviderName,
                    TenantSettingValueProvider.ProviderName),

                new SettingDefinition(
                    name: IdentitySettingNames.User.SmsResetPassword,
                    defaultValue: string.Empty,
                    displayName: L("DisplayName:Abp.Identity.User.SmsResetPassword"),
                    description: L("Description:Abp.Identity.User.SmsResetPassword"),
                    isVisibleToClients: true)
                .WithProviders(
                    DefaultValueSettingValueProvider.ProviderName,
                    ConfigurationSettingValueProvider.ProviderName,
                    GlobalSettingValueProvider.ProviderName,
                    TenantSettingValueProvider.ProviderName),

                new SettingDefinition(
                    name: IdentitySettingNames.User.SmsPhoneNumberConfirmed,
                    defaultValue: string.Empty,
                    displayName: L("DisplayName:Abp.Identity.User.SmsPhoneNumberConfirmed"),
                    description: L("Description:Abp.Identity.User.SmsPhoneNumberConfirmed"),
                    isVisibleToClients: true)
                .WithProviders(
                    DefaultValueSettingValueProvider.ProviderName,
                    ConfigurationSettingValueProvider.ProviderName,
                    GlobalSettingValueProvider.ProviderName,
                    TenantSettingValueProvider.ProviderName),

                new SettingDefinition(
                    name: IdentitySettingNames.User.SmsRepetInterval,
                    defaultValue: string.Empty,
                    displayName: L("DisplayName:Abp.Identity.User.SmsRepetInterval"),
                    description: L("Description:Abp.Identity.User.SmsRepetInterval"),
                    isVisibleToClients: true)
                .WithProviders(
                    DefaultValueSettingValueProvider.ProviderName,
                    ConfigurationSettingValueProvider.ProviderName,
                    GlobalSettingValueProvider.ProviderName,
                    TenantSettingValueProvider.ProviderName)

                ];

            context.Add(settingDefinitions);
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<IdentityResource>(name);
        }
    }
}
