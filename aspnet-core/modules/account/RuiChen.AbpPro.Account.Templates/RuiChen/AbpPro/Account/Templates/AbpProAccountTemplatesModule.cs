using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Account;
using Volo.Abp.Account.Localization;
using Volo.Abp.Emailing;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace RuiChen.AbpPro.Account
{
    [DependsOn(
        typeof(AbpEmailingModule),
        typeof(AbpProAccountApplicationContractsModule)
        )]
    public class AbpProAccountTemplatesModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<AbpProAccountTemplatesModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources.Get<AccountResource>().AddVirtualJson("/RuiChen/AbpPro/Account/Templates/Localization/Resources");
            });

        }
    }
}
