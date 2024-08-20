using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.FeatureManagement;
using Volo.Abp.FeatureManagement.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.Validation.Localization;
using VoloAbpFeatureManagementHttpApiModule = Volo.Abp.FeatureManagement.AbpFeatureManagementHttpApiModule;

namespace RuiChen.AbpPro.FeatureManagement
{
    [DependsOn(
        typeof(VoloAbpFeatureManagementHttpApiModule),
        typeof(AbpProFeatureManagementApplicationContractsModule)
        )]
    public class AbpProFeatureManagementHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(typeof(AbpFeatureManagementResource), typeof(AbpProFeatureManagementApplicationContractsModule).Assembly);
            });

            PreConfigure<IMvcBuilder>(options =>
            {
                options.AddApplicationPartIfNotExists(typeof(AbpProFeatureManagementHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources.Get<AbpFeatureManagementResource>().AddBaseTypes(typeof(AbpValidationResource));
            });
        }
    }
}
