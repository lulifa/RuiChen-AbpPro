using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace RuiChen.AbpPro.Saas
{
    [DependsOn(
        typeof(AbpProSaasDomainModule),
        typeof(AbpProSaasApplicationContractsModule)
        )]
    public class AbpProSaasApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<AbpProSaasApplicationModule>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<AbpProSaasApplicationAutoMapperProfile>(validate: true);
            });

        }
    }
}
