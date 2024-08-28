﻿using Volo.Abp.Domain.Repositories;

namespace RuiChen.AbpPro.Platform
{
    public interface IUserMenuRepository : IBasicRepository<UserMenu, Guid>
    {
        /// <summary>
        /// 用户是否拥有菜单
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="menuName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> UserHasInMenuAsync(
            Guid userId,
            string menuName,
            CancellationToken cancellationToken = default);

        Task<List<UserMenu>> GetListByUserIdAsync(
            Guid userId,
            CancellationToken cancellationToken = default);

        Task<Menu> GetStartupMenuAsync(
            Guid userId,
            CancellationToken cancellationToken = default);
    }
}
