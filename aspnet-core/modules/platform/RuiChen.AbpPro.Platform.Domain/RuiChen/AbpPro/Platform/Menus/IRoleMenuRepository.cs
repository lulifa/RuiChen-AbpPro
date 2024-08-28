using Volo.Abp.Domain.Repositories;

namespace RuiChen.AbpPro.Platform
{
    public interface IRoleMenuRepository : IBasicRepository<RoleMenu, Guid>
    {
        /// <summary>
        /// 角色是否拥有菜单
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="menuName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> RoleHasInMenuAsync(
            string roleName,
            string menuName,
            CancellationToken cancellationToken = default);

        Task<List<RoleMenu>> GetListByRoleNameAsync(
            string roleName,
            CancellationToken cancellationToken = default);

        Task<Menu> GetStartupMenuAsync(
            IEnumerable<string> roleNames,
            CancellationToken cancellationToken = default);
    }
}
