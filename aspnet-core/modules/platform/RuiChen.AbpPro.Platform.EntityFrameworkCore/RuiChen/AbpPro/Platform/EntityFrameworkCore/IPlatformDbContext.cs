using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace RuiChen.AbpPro.Platform
{
    [ConnectionStringName(PlatformDbProperties.ConnectionStringName)]
    public interface IPlatformDbContext : IEfCoreDbContext
    {
        DbSet<Menu> Menus { get; }
        DbSet<Layout> Layouts { get; }
        DbSet<RoleMenu> RoleMenus { get; }
        DbSet<UserMenu> UserMenus { get; }
        DbSet<UserFavoriteMenu> UserFavoriteMenus { get; }
        DbSet<Data> Datas { get; }
        DbSet<DataItem> DataItems { get; }
    }
}
