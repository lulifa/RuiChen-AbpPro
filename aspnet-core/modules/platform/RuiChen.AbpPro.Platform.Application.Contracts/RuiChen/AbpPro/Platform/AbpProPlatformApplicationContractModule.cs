using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace RuiChen.AbpPro.Platform
{
    [DependsOn(typeof(AbpProPlatformDomainSharedModule))]
    public class AbpProPlatformApplicationContractModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<AbpProPlatformApplicationContractModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources.Get<PlatformResource>().AddVirtualJson("/RuiChen/AbpPro/Platform/Localization/Resources");
            });
        }
    }
}
