using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.Localization;
using Volo.Abp.VirtualFileSystem;
using VoloAbpPermissionManagementApplicationContractsModule = Volo.Abp.PermissionManagement.AbpPermissionManagementApplicationContractsModule;

namespace RuiChen.AbpPro.PermissionManagement
{
    [DependsOn(
        typeof(VoloAbpPermissionManagementApplicationContractsModule)
        )]
    public class AbpProPermissionManagementApplicationContractsModule:AbpModule
    {

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<AbpProPermissionManagementApplicationContractsModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources.Get<AbpPermissionManagementResource>().AddVirtualJson("/RuiChen/AbpPro/PermissionManagement/Localization/Resources");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace(PermissionManagementErrorCodes.Namespace, typeof(AbpPermissionManagementResource));
            });

        }

    }
}
