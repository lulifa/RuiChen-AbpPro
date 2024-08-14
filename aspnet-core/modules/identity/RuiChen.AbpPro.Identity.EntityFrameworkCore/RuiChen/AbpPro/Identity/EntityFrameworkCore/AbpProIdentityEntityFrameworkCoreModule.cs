using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.Modularity;
using VoloAbpIdentityEntityFrameworkCoreModule = Volo.Abp.Identity.EntityFrameworkCore.AbpIdentityEntityFrameworkCoreModule;

namespace RuiChen.AbpPro.Identity
{
    [DependsOn(
        typeof(AbpProIdentityDomainModule),
        typeof(VoloAbpIdentityEntityFrameworkCoreModule)
        )]
    public class AbpProIdentityEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<IdentityDbContext>(options =>
            {
                options.AddRepository<IdentityRole, EfCoreIdentityRoleRepository>();
                options.AddRepository<IdentityUser, EfCoreIdentityUserRepository>();
                options.AddRepository<OrganizationUnit, EfCoreOrganizationUnitRepository>();
            });
        }
    }
}
