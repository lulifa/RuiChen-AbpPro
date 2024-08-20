using JetBrains.Annotations;
using RuiChen.AbpPro.Authorization;

namespace Volo.Abp.PermissionManagement
{
    public static class OrganizationUnitPermissionManagerExtensions
    {
        public static Task<PermissionWithGrantedProviders> GetForOrganizationUnitAsync(
            [NotNull] this IPermissionManager permissionManager,
            string organizationUnitCode,
            string permissionName)
        {
            Check.NotNull(permissionManager, nameof(permissionManager));

            return permissionManager.GetAsync(permissionName, OrganizationUnitPermissionValueProvider.ProviderName, organizationUnitCode);
        }

        public static Task<List<PermissionWithGrantedProviders>> GetAllForOrganizationUnitAsync(
            [NotNull] this IPermissionManager permissionManager,
            string organizationUnitCode)
        {
            Check.NotNull(permissionManager, nameof(permissionManager));

            return permissionManager.GetAllAsync(OrganizationUnitPermissionValueProvider.ProviderName, organizationUnitCode);
        }

        public static Task SetForOrganizationUnitAsync(
            [NotNull] this IPermissionManager permissionManager,
            string organizationUnitCode,
            [NotNull] string permissionName,
            bool isGranted)
        {
            Check.NotNull(permissionManager, nameof(permissionManager));

            return permissionManager.SetAsync(permissionName, OrganizationUnitPermissionValueProvider.ProviderName, organizationUnitCode, isGranted);
        }
    }
}
