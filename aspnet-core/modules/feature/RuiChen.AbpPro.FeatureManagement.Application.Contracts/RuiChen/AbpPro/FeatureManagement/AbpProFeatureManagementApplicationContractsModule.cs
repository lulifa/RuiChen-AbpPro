using Volo.Abp.FeatureManagement.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;
using VoloAbpFeatureManagementApplicationContractsModule = Volo.Abp.FeatureManagement.AbpFeatureManagementApplicationContractsModule;

namespace RuiChen.AbpPro.FeatureManagement
{
    [DependsOn(
        typeof(VoloAbpFeatureManagementApplicationContractsModule)
        )]
    public class AbpProFeatureManagementApplicationContractsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<AbpProFeatureManagementApplicationContractsModule>();

            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources.Get<AbpFeatureManagementResource>().AddVirtualJson("/RuiChen/AbpPro/FeatureManagement/Localization/Resources");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace(FeatureManagementErrorCodes.Namespace, typeof(AbpFeatureManagementResource));
            });

        }
    }
}
