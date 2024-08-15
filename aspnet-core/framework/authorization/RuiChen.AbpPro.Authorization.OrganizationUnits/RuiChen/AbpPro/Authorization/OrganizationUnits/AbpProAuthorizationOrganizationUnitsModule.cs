using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Authorization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Modularity;

namespace RuiChen.AbpPro.Authorization
{
    [DependsOn(
        typeof(AbpAuthorizationModule)
        )]
    public class AbpProAuthorizationOrganizationUnitsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpPermissionOptions>(options =>
            {
                options.ValueProviders.Add<OrganizationUnitPermissionValueProvider>();
            });
        }
    }
}
