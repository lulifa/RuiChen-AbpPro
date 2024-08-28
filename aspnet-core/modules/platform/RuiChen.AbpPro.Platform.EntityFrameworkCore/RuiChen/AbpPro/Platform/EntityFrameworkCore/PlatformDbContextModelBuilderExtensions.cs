using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace RuiChen.AbpPro.Platform
{
    public static class PlatformDbContextModelBuilderExtensions
    {
        public static void ConfigurePlatform(
           this ModelBuilder builder,
           Action<PlatformModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new PlatformModelBuilderConfigurationOptions(
                PlatformDbProperties.DbTablePrefix,
                PlatformDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);

            builder.Entity<Layout>(b =>
            {
                b.ToTable(options.TablePrefix + "Layouts", options.Schema);

                b.Property(p => p.Framework)
                    .HasMaxLength(LayoutConsts.MaxFrameworkLength)
                    .HasColumnName(nameof(Layout.Framework))
                    .IsRequired();

                b.ConfigureRoute();
            });

            builder.Entity<Menu>(b =>
            {
                b.ToTable(options.TablePrefix + "Menus", options.Schema);

                b.ConfigureRoute();

                b.Property(p => p.Framework)
                    .HasMaxLength(LayoutConsts.MaxFrameworkLength)
                    .HasColumnName(nameof(Menu.Framework))
                    .IsRequired();
                b.Property(p => p.Component)
                    .HasMaxLength(MenuConsts.MaxComponentLength)
                    .HasColumnName(nameof(Menu.Component))
                    .IsRequired();
                b.Property(p => p.Code)
                    .HasMaxLength(MenuConsts.MaxCodeLength)
                    .HasColumnName(nameof(Menu.Code))
                    .IsRequired();
            });

            builder.Entity<RoleMenu>(x =>
            {
                x.ToTable(options.TablePrefix + "RoleMenus");

                x.Property(p => p.RoleName)
                    .IsRequired()
                    .HasMaxLength(RoleRouteConsts.MaxRoleNameLength)
                    .HasColumnName(nameof(RoleMenu.RoleName));

                x.ConfigureByConvention();

                x.HasIndex(i => new { i.RoleName, i.MenuId });
            });

            builder.Entity<UserMenu>(x =>
            {
                x.ToTable(options.TablePrefix + "UserMenus");

                x.ConfigureByConvention();

                x.HasIndex(i => new { i.UserId, i.MenuId });
            });

            builder.Entity<UserFavoriteMenu>(x =>
            {
                x.ToTable(options.TablePrefix + "UserFavoriteMenus");

                x.Property(p => p.Framework)
                    .HasMaxLength(LayoutConsts.MaxFrameworkLength)
                    .HasColumnName(nameof(Menu.Framework))
                    .IsRequired();
                x.Property(p => p.DisplayName)
                    .HasMaxLength(RouteConsts.MaxDisplayNameLength)
                    .HasColumnName(nameof(Route.DisplayName))
                    .IsRequired();
                x.Property(p => p.Name)
                    .HasMaxLength(RouteConsts.MaxNameLength)
                    .HasColumnName(nameof(Route.Name))
                    .IsRequired();
                x.Property(p => p.Path)
                    .HasMaxLength(RouteConsts.MaxPathLength)
                    .HasColumnName(nameof(Route.Path))
                    .IsRequired();

                x.Property(p => p.Icon)
                    .HasMaxLength(UserFavoriteMenuConsts.MaxIconLength)
                    .HasColumnName(nameof(UserFavoriteMenu.Icon));
                x.Property(p => p.Color)
                    .HasMaxLength(UserFavoriteMenuConsts.MaxColorLength)
                    .HasColumnName(nameof(UserFavoriteMenu.Color));
                x.Property(p => p.AliasName)
                    .HasMaxLength(UserFavoriteMenuConsts.MaxAliasNameLength)
                    .HasColumnName(nameof(UserFavoriteMenu.AliasName));

                x.ConfigureByConvention();

                x.HasIndex(i => new { i.UserId, i.MenuId });
            });

            builder.Entity<Data>(x =>
            {
                x.ToTable(options.TablePrefix + "Datas");

                x.Property(p => p.Code)
                    .HasMaxLength(DataConsts.MaxCodeLength)
                    .HasColumnName(nameof(Data.Code))
                    .IsRequired();
                x.Property(p => p.Name)
                    .HasMaxLength(DataConsts.MaxNameLength)
                    .HasColumnName(nameof(Data.Name))
                    .IsRequired();
                x.Property(p => p.DisplayName)
                   .HasMaxLength(DataConsts.MaxDisplayNameLength)
                   .HasColumnName(nameof(Data.DisplayName))
                   .IsRequired();
                x.Property(p => p.Description)
                    .HasMaxLength(DataConsts.MaxDescriptionLength)
                    .HasColumnName(nameof(Data.Description));

                x.ConfigureByConvention();

                x.HasMany(p => p.Items)
                    .WithOne()
                    .HasForeignKey(fk => fk.DataId)
                    .IsRequired();

                x.HasIndex(i => new { i.Name });
            });

            builder.Entity<DataItem>(x =>
            {
                x.ToTable(options.TablePrefix + "DataItems");

                x.Property(p => p.DefaultValue)
                    .HasMaxLength(DataItemConsts.MaxValueLength)
                    .HasColumnName(nameof(DataItem.DefaultValue));
                x.Property(p => p.Name)
                    .HasMaxLength(DataItemConsts.MaxNameLength)
                    .HasColumnName(nameof(DataItem.Name))
                    .IsRequired();
                x.Property(p => p.DisplayName)
                   .HasMaxLength(DataItemConsts.MaxDisplayNameLength)
                   .HasColumnName(nameof(DataItem.DisplayName))
                   .IsRequired();
                x.Property(p => p.Description)
                    .HasMaxLength(DataItemConsts.MaxDescriptionLength)
                    .HasColumnName(nameof(DataItem.Description));

                x.Property(p => p.AllowBeNull).HasDefaultValue(true);

                x.ConfigureByConvention();

                x.HasIndex(i => new { i.Name });
            });
        }

        public static EntityTypeBuilder<TRoute> ConfigureRoute<TRoute>(
            this EntityTypeBuilder<TRoute> builder)
            where TRoute : Route
        {
            builder
                .Property(p => p.DisplayName)
                .HasMaxLength(RouteConsts.MaxDisplayNameLength)
                .HasColumnName(nameof(Route.DisplayName))
                .IsRequired();
            builder
                .Property(p => p.Name)
                .HasMaxLength(RouteConsts.MaxNameLength)
                .HasColumnName(nameof(Route.Name))
                .IsRequired();
            builder
                .Property(p => p.Path)
                .HasMaxLength(RouteConsts.MaxPathLength)
                .HasColumnName(nameof(Route.Path));
            builder
                .Property(p => p.Redirect)
                .HasMaxLength(RouteConsts.MaxRedirectLength)
                .HasColumnName(nameof(Route.Redirect));

            builder.ConfigureByConvention();

            return builder;
        }

        public static OwnedNavigationBuilder<TEntity, TRoute> ConfigureRoute<TEntity, TRoute>(
            [NotNull] this OwnedNavigationBuilder<TEntity, TRoute> builder,
            [CanBeNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            where TEntity : class
            where TRoute : Route
        {
            builder.ToTable(tablePrefix + "Routes", schema);

            builder
                .Property(p => p.DisplayName)
                .HasMaxLength(RouteConsts.MaxDisplayNameLength)
                .HasColumnName(nameof(Route.DisplayName))
                .IsRequired();
            builder
                .Property(p => p.Name)
                .HasMaxLength(RouteConsts.MaxNameLength)
                .HasColumnName(nameof(Route.Name))
                .IsRequired();
            builder
                .Property(p => p.Path)
                .HasMaxLength(RouteConsts.MaxPathLength)
                .HasColumnName(nameof(Route.Path));
            builder
                .Property(p => p.Redirect)
                .HasMaxLength(RouteConsts.MaxRedirectLength)
                .HasColumnName(nameof(Route.Redirect));

            return builder;
        }
    }
}
