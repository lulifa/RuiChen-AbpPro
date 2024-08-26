using Localization.Resources.AbpUi;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace RuiChen.AbpPro.CachingManagement
{
    [DependsOn(
        typeof(AbpAspNetCoreMvcModule),
        typeof(AbpProCachingManagementApplicationContractsModule)
        )]
    public class AbpProCachingManagementHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(typeof(CacheResource), typeof(AbpProCachingManagementApplicationContractsModule).Assembly);
            });

            PreConfigure<IMvcBuilder>(options =>
            {
                options.AddApplicationPartIfNotExists(typeof(AbpProCachingManagementHttpApiModule).Assembly);
            });

        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources.Get<CacheResource>().AddBaseTypes(typeof(AbpUiResource));
            });
        }

    }
}
