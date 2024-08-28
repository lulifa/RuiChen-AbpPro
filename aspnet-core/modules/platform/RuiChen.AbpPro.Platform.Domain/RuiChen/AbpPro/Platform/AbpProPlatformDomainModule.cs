using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.BlobStoring;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending.Modularity;

namespace RuiChen.AbpPro.Platform
{
    [DependsOn(
        typeof(AbpProPlatformDomainSharedModule),
        typeof(AbpBlobStoringModule),
        typeof(AbpEventBusModule))]
    public class AbpProPlatformDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<AbpProPlatformDomainModule>();

            Configure<DataItemMappingOptions>(options =>
            {
                options.SetDefaultMapping();
            });

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<PlatformDomainMappingProfile>(validate: true);
            });

            Configure<AbpDistributedEntityEventOptions>(options =>
            {
                options.EtoMappings.Add<Layout, LayoutEto>(typeof(AbpProPlatformDomainModule));

                options.EtoMappings.Add<Menu, MenuEto>(typeof(AbpProPlatformDomainModule));
                options.EtoMappings.Add<UserMenu, UserMenuEto>(typeof(AbpProPlatformDomainModule));
                options.EtoMappings.Add<RoleMenu, RoleMenuEto>(typeof(AbpProPlatformDomainModule));

            });
        }
        public override void PostConfigureServices(ServiceConfigurationContext context)
        {
            ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity(
                PlatformModuleExtensionConsts.ModuleName,
                PlatformModuleExtensionConsts.EntityNames.Route,
                typeof(Route)
            );
        }
    }
}
