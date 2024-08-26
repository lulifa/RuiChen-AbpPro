using Microsoft.Extensions.DependencyInjection;
using RuiChen.AbpPro.AuditLogging;
using RuiChen.AbpPro.Logging;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace RuiChen.AbpPro.Auditing
{
    [DependsOn(
        typeof(AbpAutoMapperModule),
        typeof(AbpProLoggingModule),
        typeof(AbpProAuditLoggingModule),
        typeof(AbpProAuditingApplicationContractsModule))]
    public class AbpProAuditingApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<AbpProAuditingApplicationModule>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<AbpProAuditingMapperProfile>(validate: true);
            });
        }
    }
}
