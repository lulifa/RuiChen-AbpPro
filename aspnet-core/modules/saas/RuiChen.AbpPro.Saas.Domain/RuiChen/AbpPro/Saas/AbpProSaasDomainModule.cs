using Microsoft.Extensions.DependencyInjection;
using RuiChen.AbpPro.MultiTenancy;
using Volo.Abp.AutoMapper;
using Volo.Abp.Domain;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending.Modularity;
using Volo.Abp.Threading;

namespace RuiChen.AbpPro.Saas
{
    [DependsOn(
        typeof(AbpAutoMapperModule),
        typeof(AbpDddDomainModule),
        typeof(AbpProSaasDomainSharedModule),
        typeof(AbpProMultiTenancyEditionsModule)
        )]
    public class AbpProSaasDomainModule:AbpModule
    {
        private static readonly OneTimeRunner OneTimeRunner = new();

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<AbpProSaasDomainModule>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<AbpProSaasDomainMapperProfile>(validate: true);
            });

            Configure<AbpDistributedEntityEventOptions>(options =>
            {
                options.EtoMappings.Add<Edition, EditionEto>();
                options.EtoMappings.Add<Tenant, TenantEto>();

                options.AutoEventSelectors.Add<Edition>();
                options.AutoEventSelectors.Add<Tenant>();
            });
        }

        public override void PostConfigureServices(ServiceConfigurationContext context)
        {
            OneTimeRunner.Run(() =>
            {
                ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity(
                    SaasModuleExtensionConsts.ModuleName,
                    SaasModuleExtensionConsts.EntityNames.Edition,
                    typeof(Edition)
                );
                ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity(
                    SaasModuleExtensionConsts.ModuleName,
                    SaasModuleExtensionConsts.EntityNames.Tenant,
                    typeof(Tenant)
                );
            });
        }

    }
}
