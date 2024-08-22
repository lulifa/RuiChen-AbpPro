using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.Authorization;
using Volo.Abp.Localization;
using Volo.Abp.Localization.Resources.AbpLocalization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace RuiChen.AbpPro.LocalizationManagement
{
    [DependsOn(
        typeof(AbpAuthorizationModule),
        typeof(AbpAspNetCoreMvcModule),
        typeof(AbpProLocalizationManagementDomainSharedModule)
        )]
    public class AbpProLocalizationManagementApplicationContractsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(typeof(AbpLocalizationResource), typeof(AbpProLocalizationManagementApplicationContractsModule).Assembly);
            });

            PreConfigure<IMvcBuilder>(options =>
            {
                options.AddApplicationPartIfNotExists(typeof(AbpProLocalizationManagementApplicationContractsModule).Assembly);
            });

        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<AbpProLocalizationManagementApplicationContractsModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources.Get<AbpLocalizationResource>().AddVirtualJson("/RuiChen/AbpPro/AspNetCore/Mvc/Localization/Resources");
            });

        }

    }
}
