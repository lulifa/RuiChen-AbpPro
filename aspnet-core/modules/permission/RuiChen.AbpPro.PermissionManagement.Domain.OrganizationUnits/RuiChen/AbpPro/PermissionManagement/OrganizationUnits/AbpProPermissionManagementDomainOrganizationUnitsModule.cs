using RuiChen.AbpPro.Authorization;
using RuiChen.AbpPro.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;

namespace RuiChen.AbpPro.PermissionManagement
{
    [DependsOn(
        typeof(AbpProIdentityDomainModule),
        typeof(AbpPermissionManagementDomainModule),
        typeof(AbpProAuthorizationOrganizationUnitsModule)
        )]
    public class AbpProPermissionManagementDomainOrganizationUnitsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<PermissionManagementOptions>(options =>
            {
                options.ManagementProviders.Add<OrganizationUnitPermissionManagementProvider>();

                options.ProviderPolicies[OrganizationUnitPermissionValueProvider.ProviderName] = "AbpIdentity.OrganizationUnits.ManagePermissions";

            });
        }
    }
}
