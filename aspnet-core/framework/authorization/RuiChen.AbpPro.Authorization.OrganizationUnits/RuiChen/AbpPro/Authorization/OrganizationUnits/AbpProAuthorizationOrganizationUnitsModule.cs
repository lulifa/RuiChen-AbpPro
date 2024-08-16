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
        /// <summary>
        /// AbpPermissionOptions：这是一个权限相关的配置选项类，通常用于配置与权限管理有关的各种设置
        /// 将 OrganizationUnitPermissionValueProvider 加入到 AbpPermissionOptions 的 ValueProviders 中，从而实现基于组织单元的权限管理。
        /// 这让你的应用程序能够根据组织单元的不同动态地管理权限，适用于多层次组织结构的复杂权限控制场景
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpPermissionOptions>(options =>
            {
                options.ValueProviders.Add<OrganizationUnitPermissionValueProvider>();
            });
        }
    }
}
