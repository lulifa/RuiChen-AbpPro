using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement;
using Volo.Abp.SettingManagement.Localization;
using Volo.Abp.VirtualFileSystem;

namespace RuiChen.AbpPro.SettingManagement
{
    [DependsOn(
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpSettingManagementDomainSharedModule)
        )]
    public class AbpProSettingManagementApplicationContractsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            AutoAddSettingProviders(context.Services);
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<AbpProSettingManagementApplicationContractsModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources.Get<AbpSettingManagementResource>().AddVirtualJson("/RuiChen/AbpPro/SettingManagement/Localization/Resources");
            });
        }

        private static void AutoAddSettingProviders(IServiceCollection services)
        {
            var userSettingProviders = new List<Type>();
            var globalSettingProviders = new List<Type>();

            services.OnRegistered(context =>
            {
                if (typeof(IUserSettingAppService).IsAssignableFrom(context.ImplementationType) &&
                    context.ImplementationType.Name.EndsWith("AppService"))
                {
                    userSettingProviders.Add(context.ImplementationType);
                }
                if (typeof(IReadonlySettingAppService).IsAssignableFrom(context.ImplementationType) &&
                    context.ImplementationType.Name.EndsWith("AppService"))
                {
                    globalSettingProviders.Add(context.ImplementationType);
                }
            });

            services.Configure<SettingManagementMergeOptions>(options =>
            {
                options.UserSettingProviders.AddIfNotContains(userSettingProviders);
                options.GlobalSettingProviders.AddIfNotContains(globalSettingProviders);
            });
        }
    }
}
