using RuiChen.AbpPro.Admin.EntityFrameworkCore;
using RuiChen.AbpPro.Platform.Navigation.VuePureAdmin;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace RuiChen.AbpPro.Admin.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpProNavigationVuePureAdminModule),
        typeof(RuiChenAbpProAdminMigrationEntityFrameworkCoreModule)
        )]
    public partial class RuiChenAbpProAdminDbMigratorModule:AbpModule
    {
    }
}
