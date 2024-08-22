using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Localization.Resources.AbpLocalization;
using Volo.Abp.Modularity;
using Volo.Abp.Validation.Localization;

namespace RuiChen.AbpPro.LocalizationManagement
{
    [DependsOn(
        typeof(AbpProLocalizationManagementApplicationContractsModule)
        )]
    public class AbpProLocalizationManagementHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(typeof(LocalizationManagementResource), typeof(AbpProLocalizationManagementApplicationContractsModule).Assembly);
            });

            PreConfigure<IMvcBuilder>(options =>
            {
                options.AddApplicationPartIfNotExists(typeof(AbpProLocalizationManagementApplicationContractsModule).Assembly);
            });

        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources.Get<LocalizationManagementResource>().AddBaseTypes(typeof(AbpValidationResource), typeof(AbpLocalizationResource));
            });
        }
    }
}
