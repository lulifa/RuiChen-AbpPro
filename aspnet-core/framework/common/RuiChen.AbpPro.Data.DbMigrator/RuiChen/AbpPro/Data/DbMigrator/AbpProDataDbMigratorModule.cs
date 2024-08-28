using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace RuiChen.AbpPro.Data.DbMigrator
{
    [DependsOn(
        typeof(AbpEntityFrameworkCoreModule))]
    public class AbpProDataDbMigratorModule : AbpModule
    {

    }
}
