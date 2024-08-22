using Microsoft.Extensions.DependencyInjection;
using RuiChen.AbpPro.Localization;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace RuiChen.AbpPro.LocalizationManagement
{
    [DependsOn(
        typeof(AbpDddApplicationModule),
        typeof(AbpProLocalizationCultureMapModule),
        typeof(AbpProLocalizationManagementApplicationContractsModule)
        )]
    public class AbpProLocalizationManagementApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<AbpProLocalizationManagementApplicationModule>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<LocalizationManagementApplicationMapperProfile>(validate: true);
            });

        }
    }
}
