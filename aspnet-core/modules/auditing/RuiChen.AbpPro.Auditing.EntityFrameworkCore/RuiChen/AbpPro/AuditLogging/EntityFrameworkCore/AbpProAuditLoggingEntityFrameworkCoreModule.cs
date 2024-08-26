using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using VoloAbpIdentityEntityFrameworkCoreModule = Volo.Abp.Identity.EntityFrameworkCore.AbpIdentityEntityFrameworkCoreModule;
using VoloAbpAuditLoggingEntityFrameworkCoreModule = Volo.Abp.AuditLogging.EntityFrameworkCore.AbpAuditLoggingEntityFrameworkCoreModule;

namespace RuiChen.AbpPro.AuditLogging
{
    [DependsOn(
        typeof(VoloAbpIdentityEntityFrameworkCoreModule),
        typeof(VoloAbpAuditLoggingEntityFrameworkCoreModule),
        typeof(AbpProAuditLoggingModule),
        typeof(AbpAutoMapperModule))]
    public class AbpProAuditLoggingEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<AbpProAuditLoggingEntityFrameworkCoreModule>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<AbpAuditingMapperProfile>(validate: true);
            });
        }
    }
}
