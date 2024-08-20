using Volo.Abp.Authorization.Permissions;

namespace Volo.Abp.Authorization
{
    public static class IPermissionDefinitionManagerExtensions
    {
        public async static Task<PermissionGroupDefinition> GetGroupOrNullAsync(
            this IPermissionDefinitionManager permissionDefinitionManager,
            string name)
        {
            var groups = await permissionDefinitionManager.GetGroupsAsync();

            return groups.FirstOrDefault(x => x.Name == name);
        }
    }
}
