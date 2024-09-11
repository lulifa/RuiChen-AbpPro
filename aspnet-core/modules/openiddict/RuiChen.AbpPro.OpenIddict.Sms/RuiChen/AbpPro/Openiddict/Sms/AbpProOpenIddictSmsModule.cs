using Microsoft.Extensions.DependencyInjection;
using RuiChen.AbpPro.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict;
using Volo.Abp.OpenIddict.ExtensionGrantTypes;
using Volo.Abp.OpenIddict.Localization;
using Volo.Abp.Sms;
using Volo.Abp.VirtualFileSystem;

namespace RuiChen.AbpPro.OpenIddict.Sms
{
    [DependsOn(
        typeof(AbpSmsModule),
        typeof(AbpOpenIddictAspNetCoreModule),
        typeof(AbpProIdentityDomainModule))]
    public class AbpProOpenIddictSmsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<OpenIddictServerBuilder>(builder =>
            {
                builder.AllowSmsFlow();
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpOpenIddictExtensionGrantsOptions>(options =>
            {
                options.Grants.TryAdd(SmsTokenExtensionGrantConsts.GrantType, new SmsTokenExtensionGrant());
            });

            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<AbpProOpenIddictSmsModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources.Get<AbpOpenIddictResource>().AddVirtualJson("/RuiChen/AbpPro/OpenIddict/Sms/Localization/Resources");
            });
        }
    }
}