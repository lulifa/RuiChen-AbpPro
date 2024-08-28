using Microsoft.Extensions.DependencyInjection;
using RuiChen.AbpPro.Platform.Navigation.VuePureAdmin;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace RuiChen.AbpPro.Platform
{
    [DependsOn(
        typeof(AbpProPlatformApplicationContractModule),
        typeof(AbpProNavigationVuePureAdminModule)
        )]
    public class AbpProPlatformApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<AbpProPlatformApplicationModule>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<PlatformApplicationMappingProfile>(validate: true);
            });
        }
    }
}
