using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace RuiChen.AbpPro.Platform
{
    [DependsOn(
        typeof(AbpLocalizationModule),
        typeof(AbpMultiTenancyAbstractionsModule)
        )]
    public class AbpProPlatformDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<AbpProPlatformDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources.Add<PlatformResource>("en")
                                 .AddBaseTypes(typeof(AbpValidationResource))
                                 .AddVirtualJson("/RuiChen/AbpPro/Platform/Localization/Resources");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace(PlatformErrorCodes.Namespace, typeof(PlatformResource));
            });

        }
    }
}
