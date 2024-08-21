using RuiChen.AbpPro.Saas.Localization;
using Volo.Abp.Auditing;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.Validation;
using Volo.Abp.VirtualFileSystem;

namespace RuiChen.AbpPro.Saas
{
    [DependsOn(
        typeof(AbpValidationModule),
        typeof(AbpAuditingContractsModule)
        )]
    public class AbpProSaasDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<AbpProSaasDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources.Add<AbpProSaasResource>().AddVirtualJson("/RuiChen/AbpPro/Saas/Localization/Resources");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace(AbpProSaasErrorCodes.Namespace, typeof(AbpProSaasResource));
                // 见租户管理模块
                options.MapCodeNamespace(AbpProSaasErrorCodes.NamespaceMultiTenancy, typeof(AbpProSaasResource));
            });

        }
    }
}
