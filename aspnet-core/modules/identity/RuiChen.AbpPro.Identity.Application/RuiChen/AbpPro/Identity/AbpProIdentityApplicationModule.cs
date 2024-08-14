using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using VoloAbpIdentityApplicationModule = Volo.Abp.Identity.AbpIdentityApplicationModule;

namespace RuiChen.AbpPro.Identity
{
    [DependsOn(
        typeof(VoloAbpIdentityApplicationModule),
        typeof(AbpProIdentityApplicationContractsModule),
        typeof(AbpProIdentityDomainModule)
        )]
    public class AbpProIdentityApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<AbpProIdentityApplicationModule>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<AbpProIdentityApplicationModuleAutoMapperProfile>(validate: true);
            });

        }
    }
}
