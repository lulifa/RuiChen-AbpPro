using Microsoft.Extensions.DependencyInjection;
using RuiChen.AbpPro.AuditLogging;
using RuiChen.AbpPro.Data.DbMigrator;
using RuiChen.AbpPro.Identity;
using RuiChen.AbpPro.LocalizationManagement;
using RuiChen.AbpPro.Platform;
using RuiChen.AbpPro.Saas;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;

namespace RuiChen.AbpPro.Admin.EntityFrameworkCore
{
    [DependsOn(
        typeof(AbpProAuditLoggingEntityFrameworkCoreModule),
        typeof(AbpFeatureManagementEntityFrameworkCoreModule),
        typeof(AbpProIdentityEntityFrameworkCoreModule),
        typeof(AbpProLocalizationManagementEntityFrameworkCoreModule),
        typeof(AbpOpenIddictEntityFrameworkCoreModule),
        typeof(AbpPermissionManagementEntityFrameworkCoreModule),
        typeof(AbpProPlatformEntityFrameworkCoreModule),
        typeof(AbpProSaasEntityFrameworkCoreModule),
        typeof(AbpSettingManagementEntityFrameworkCoreModule),
        typeof(AbpEntityFrameworkCoreMySQLModule),
        typeof(AbpProDataDbMigratorModule)
        )]
    public class RuiChenAbpProAdminMigrationEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<RuiChenAbpProAdminMigrationDbContext>();

            Configure<AbpDbContextOptions>(options =>
            {
                options.UseMySQL();
            });
        }
    }
}
