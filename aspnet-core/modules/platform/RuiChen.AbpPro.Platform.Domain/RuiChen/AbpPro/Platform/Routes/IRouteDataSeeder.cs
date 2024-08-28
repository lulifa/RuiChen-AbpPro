﻿namespace RuiChen.AbpPro.Platform
{
    public interface IRouteDataSeeder
    {
        Task<Layout> SeedLayoutAsync(
            string name,
            string path,
            string displayName,
            Guid dataId,
            string framework,
            string redirect = "",
            string description = "",
            Guid? tenantId = null,
            CancellationToken cancellationToken = default);

        Task<Menu> SeedMenuAsync(
            Layout layout,
            string name,
            string path,
            string code,
            string component,
            string displayName,
            string redirect = "",
            string description = "",
            Guid? parentId = null,
            Guid? tenantId = null,
            bool isPublic = false,
            CancellationToken cancellationToken = default);

        Task SeedUserMenuAsync(
            Guid userId,
            Menu menu,
            Guid? tenantId = null,
            CancellationToken cancellationToken = default
            );

        Task SeedRoleMenuAsync(
           string roleName,
           Menu menu,
           Guid? tenantId = null,
           CancellationToken cancellationToken = default
           );
    }
}
