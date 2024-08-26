using Localization.Resources.AbpUi;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AuditLogging.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace RuiChen.AbpPro.Auditing
{
    [DependsOn(
        typeof(AbpAspNetCoreMvcModule),
        typeof(AbpProAuditingApplicationContractsModule))]
    public class AbpProAuditingHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(AbpProAuditingHttpApiModule).Assembly);
            });

            PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(typeof(AuditLoggingResource), typeof(AbpProAuditingApplicationContractsModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources.Get<AuditLoggingResource>().AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
