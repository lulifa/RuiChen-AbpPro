using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace RuiChen.AbpPro.Platform
{
    [DependsOn(
        typeof(AbpProPlatformDomainModule),
        typeof(AbpEntityFrameworkCoreModule))]
    public class AbpProPlatformEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<PlatformDbContext>(options =>
            {
                options.AddRepository<Data, EfCoreDataRepository>();
                options.AddRepository<Menu, EfCoreMenuRepository>();
                options.AddRepository<UserMenu, EfCoreUserMenuRepository>();
                options.AddRepository<RoleMenu, EfCoreRoleMenuRepository>();
                options.AddRepository<UserFavoriteMenu, EfCoreUserFavoriteMenuRepository>();
                options.AddRepository<Layout, EfCoreLayoutRepository>();

                options.AddDefaultRepositories(includeAllEntities: true);
            });
        }
    }
}
