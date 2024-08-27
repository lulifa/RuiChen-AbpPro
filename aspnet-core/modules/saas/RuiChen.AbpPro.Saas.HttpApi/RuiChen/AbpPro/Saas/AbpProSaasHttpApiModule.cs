using Localization.Resources.AbpUi;
using Microsoft.Extensions.DependencyInjection;
using RuiChen.AbpPro.Saas.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace RuiChen.AbpPro.Saas
{
    [DependsOn(
        typeof(AbpAspNetCoreMvcModule),
        typeof(AbpProSaasApplicationContractsModule)
        )]
    public class AbpProSaasHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(typeof(AbpProSaasResource), typeof(AbpProSaasApplicationContractsModule).Assembly);
            });

            PreConfigure<IMvcBuilder>(options =>
            {
                options.AddApplicationPartIfNotExists(typeof(AbpProSaasHttpApiModule).Assembly);
            });

        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources.Get<AbpProSaasResource>().AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
