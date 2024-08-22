using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace RuiChen.AbpPro.LocalizationManagement
{
    [DependsOn(
        typeof(AbpEntityFrameworkCoreModule),
        typeof(AbpProLocalizationManagementDomainModule)
        )]
    public class AbpProLocalizationManagementEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<LocalizationDbContext>(options =>
            {
                options.AddRepository<Language, EfCoreLanguageRepository>();
                options.AddRepository<LanguageText, EfCoreLanguageTextRepository>();
                options.AddRepository<Resource, EfCoreResourceRepository>();

                options.AddDefaultRepositories(includeAllEntities: true);

            });
        }
    }
}
