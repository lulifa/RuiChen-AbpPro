using Microsoft.EntityFrameworkCore;
using RuiChen.AbpPro.LocalizationManagement;
using RuiChen.AbpPro.Platform;
using RuiChen.AbpPro.Saas;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;

namespace RuiChen.AbpPro.Admin.EntityFrameworkCore
{
    public class RuiChenAbpProAdminMigrationDbContext : AbpDbContext<RuiChenAbpProAdminMigrationDbContext>
    {
        public RuiChenAbpProAdminMigrationDbContext(DbContextOptions<RuiChenAbpProAdminMigrationDbContext> options)
        : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureAuditLogging();
            modelBuilder.ConfigureIdentity();
            modelBuilder.ConfigureOpenIddict();
            modelBuilder.ConfigureSaas();
            modelBuilder.ConfigureFeatureManagement();
            modelBuilder.ConfigureSettingManagement();
            modelBuilder.ConfigurePermissionManagement();
            modelBuilder.ConfigurePlatform();
            modelBuilder.ConfigureLocalization();
        }
    }
}
