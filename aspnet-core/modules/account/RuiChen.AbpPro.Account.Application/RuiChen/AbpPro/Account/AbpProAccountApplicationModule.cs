using RuiChen.AbpPro.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.VirtualFileSystem;
using VoloAbpAccountApplicationModule = Volo.Abp.Account.AbpAccountApplicationModule;

namespace RuiChen.AbpPro.Account
{
    [DependsOn(
        typeof(VoloAbpAccountApplicationModule),
        typeof(AbpProAccountApplicationContractsModule),
        typeof(AbpProAccountTemplatesModule),
        typeof(AbpProIdentityDomainModule)
        )]
    public class AbpProAccountApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<AbpProAccountApplicationModule>();
            });

            Configure<AppUrlOptions>(options =>
            {
                options.Applications["MVC"].Urls[AccountUrlNames.EmailConfirm] = "Account/EmailConfirm";
            });
        }
    }
}
