using RuiChen.AbpPro.UI.Navigation;
using Volo.Abp.Data;
using Volo.Abp.MultiTenancy;

namespace RuiChen.AbpPro.Platform.Navigation.VuePureAdmin
{
    public class NavigationVuePureAdminDefinitionProvider : NavigationDefinitionProvider
    {

        public override void Define(INavigationDefinitionContext context)
        {
            context.Add(GetDashboard());
            context.Add(GetManage());
            context.Add(GetSaas());
            context.Add(GetPlatform());
            context.Add(GetLocalization());
        }

        private static NavigationDefinition GetDashboard()
        {
            var dashboard = new ApplicationMenu(
                name: "Pure Dashboard",
                displayName: "仪表盘",
                url: "/dashboard",
                component: "",
                description: "仪表盘",
                icon: "ion:grid-outline",
                redirect: "/dashboard/workbench");

            dashboard.AddItem(
                new ApplicationMenu(
                    name: "Analysis",
                    displayName: "分析页",
                    url: "/dashboard/analysis",
                    component: "/dashboard/analysis/index",
                    description: "分析页"));
            dashboard.AddItem(
               new ApplicationMenu(
                   name: "Workbench",
                   displayName: "工作台",
                   url: "/dashboard/workbench",
                   component: "/dashboard/workbench/index",
                   description: "工作台"));


            return new NavigationDefinition(dashboard);
        }

        private static NavigationDefinition GetManage()
        {
            var manage = new ApplicationMenu(
                name: "Manage",
                displayName: "管理",
                url: "/manage",
                component: "",
                description: "管理",
                icon: "ant-design:control-outlined");

            var identity = manage.AddItem(
                new ApplicationMenu(
                    name: "Identity",
                    displayName: "身份认证管理",
                    url: "/manage/identity",
                    component: "",
                    description: "身份认证管理"));
            identity.AddItem(
              new ApplicationMenu(
                  name: "User",
                  displayName: "用户",
                  url: "/manage/identity/user",
                  component: "/identity/user/index",
                  description: "用户"));
            identity.AddItem(
              new ApplicationMenu(
                  name: "Role",
                  displayName: "角色",
                  url: "/manage/identity/role",
                  component: "/identity/role/index",
                  description: "角色"));
            identity.AddItem(
              new ApplicationMenu(
                  name: "Claim",
                  displayName: "身份标识",
                  url: "/manage/identity/claim-types",
                  component: "/identity/claim-types/index",
                  description: "身份标识",
                  multiTenancySides: MultiTenancySides.Host));
            identity.AddItem(
              new ApplicationMenu(
                  name: "OrganizationUnits",
                  displayName: "组织机构",
                  url: "/manage/identity/organization-units",
                  component: "/identity/organization-units/index",
                  description: "组织机构"));
            identity.AddItem(
              new ApplicationMenu(
                  name: "SecurityLogs",
                  displayName: "安全日志",
                  url: "/manage/identity/security-logs",
                  component: "/identity/security-logs/index",
                  description: "安全日志")
                // 此路由需要依赖安全日志特性
                .SetProperty("requiredFeatures", "AbpAuditing.Logging.SecurityLog"));

            manage.AddItem(new ApplicationMenu(
                   name: "AuditLogs",
                   displayName: "审计日志",
                   url: "/manage/audit-logs",
                   component: "/auditing/index",
                   description: "审计日志")
                // 此路由需要依赖审计日志特性
                .SetProperty("requiredFeatures", "AbpAuditing.Logging.AuditLog"));

            var settingManagement = manage.AddItem(new ApplicationMenu(
                   name: "SettingManagement",
                   displayName: "设置管理",
                   url: "/manage/settings",
                   component: "LAYOUT",
                   description: "设置管理",
                   icon: "ant-design:setting-outlined",
                   multiTenancySides: MultiTenancySides.Host)
                // 此路由需要依赖设置管理特性
                .SetProperty("requiredFeatures", "SettingManagement.Enable"));
            settingManagement.AddItem(new ApplicationMenu(
                   name: "SystemSettings",
                   displayName: "系统设置",
                   url: "/manage/settings/system-setting",
                   component: "/settings-management/settings/index",
                   description: "系统设置",
                   multiTenancySides: MultiTenancySides.Host));
            settingManagement.AddItem(new ApplicationMenu(
                   name: "SettingDefinitions",
                   displayName: "设置定义",
                   url: "/manage/settings/definitions",
                   component: "/settings-management/definitions/index",
                   description: "设置定义",
                   multiTenancySides: MultiTenancySides.Host));

            var featureManagement = manage.AddItem(new ApplicationMenu(
                   name: "FeaturesManagement",
                   displayName: "功能管理",
                   url: "/manage/feature-management",
                   component: "LAYOUT",
                   description: "功能管理",
                   icon: "ant-design:gold-outlined",
                   multiTenancySides: MultiTenancySides.Host));
            featureManagement.AddItem(new ApplicationMenu(
                   name: "FeaturesGroupDefinitions",
                   displayName: "功能分组",
                   url: "/manage/feature-management/definitions/groups",
                   component: "/feature-management/definitions/groups/index",
                   description: "功能分组",
                   multiTenancySides: MultiTenancySides.Host));
            featureManagement.AddItem(new ApplicationMenu(
                   name: "FeaturesDefinitions",
                   displayName: "功能定义",
                   url: "/manage/feature-management/definitions/features",
                   component: "/feature-management/definitions/features/index",
                   description: "功能定义",
                   multiTenancySides: MultiTenancySides.Host));

            var permissionManagement = manage.AddItem(new ApplicationMenu(
                  name: "PermissionsManagement",
                  displayName: "权限管理",
                  url: "/manage/permission-management",
                  component: "LAYOUT",
                  description: "权限管理",
                  icon: "arcticons:permissionsmanager",
                  multiTenancySides: MultiTenancySides.Host));
            permissionManagement.AddItem(new ApplicationMenu(
                   name: "PermissionsGroupDefinitions",
                   displayName: "权限分组",
                   url: "/manage/permission-management/definitions/groups",
                   component: "/permission-management/definitions/groups/index",
                   description: "权限分组",
                   multiTenancySides: MultiTenancySides.Host));
            permissionManagement.AddItem(new ApplicationMenu(
                   name: "PermissionsDefinitions",
                   displayName: "权限定义",
                   url: "/manage/permission-management/definitions/permissions",
                   component: "/permission-management/definitions/permissions/index",
                   description: "权限定义",
                   multiTenancySides: MultiTenancySides.Host));

            var notificationManagement = manage.AddItem(new ApplicationMenu(
                  name: "RealtimeNotifications",
                  displayName: "通知管理",
                  url: "/realtime/notifications",
                  component: "LAYOUT",
                  description: "通知管理",
                  icon: "ant-design:notification-outlined",
                  multiTenancySides: MultiTenancySides.Host));
            notificationManagement.AddItem(new ApplicationMenu(
                   name: "NotificationsGroupDefinitions",
                   displayName: "通知分组",
                   url: "/realtime/notifications/definitions/groups",
                   component: "/realtime/notifications/definitions/groups/index",
                   description: "通知分组",
                   multiTenancySides: MultiTenancySides.Host));
            notificationManagement.AddItem(new ApplicationMenu(
                   name: "NotificationsDefinitions",
                   displayName: "通知定义",
                   url: "/realtime/notifications/definitions/notifications",
                   component: "/realtime/notifications/definitions/notifications/index",
                   description: "通知定义",
                   multiTenancySides: MultiTenancySides.Host));

            var openIddict = manage.AddItem(
                new ApplicationMenu(
                    name: "OpenIddict",
                    displayName: "身份认证服务器",
                    url: "/manage/openiddict",
                    component: "LAYOUT",
                    description: "身份认证服务器(OpenIddict)",
                    multiTenancySides: MultiTenancySides.Host));
            openIddict.AddItem(
                new ApplicationMenu(
                    name: "OpenIddictApplications",
                    displayName: "应用管理",
                    url: "/manage/openiddict/applications",
                    component: "/openiddict/applications/index",
                    description: "应用管理",
                    multiTenancySides: MultiTenancySides.Host));
            openIddict.AddItem(
                new ApplicationMenu(
                    name: "OpenIddictAuthorizations",
                    displayName: "授权管理",
                    url: "/manage/openiddict/authorizations",
                    component: "/openiddict/authorizations/index",
                    description: "授权管理",
                    multiTenancySides: MultiTenancySides.Host));
            openIddict.AddItem(
                new ApplicationMenu(
                    name: "OpenIddictScopes",
                    displayName: "Api 范围",
                    url: "/manage/openiddict/scopes",
                    component: "/openiddict/scopes/index",
                    description: "Api 范围",
                    multiTenancySides: MultiTenancySides.Host));
            openIddict.AddItem(
                new ApplicationMenu(
                    name: "OpenIddictTokens",
                    displayName: "授权令牌",
                    url: "/manage/openiddict/tokens",
                    component: "/openiddict/tokens/index",
                    description: "授权令牌",
                    multiTenancySides: MultiTenancySides.Host));

            manage.AddItem(
                new ApplicationMenu(
                    name: "Logs",
                    displayName: "系统日志",
                    url: "/sys/logs",
                    component: "/sys/logging/index",
                    description: "系统日志",
                    multiTenancySides: MultiTenancySides.Host));

            manage.AddItem(
                new ApplicationMenu(
                    name: "ApiDocument",
                    displayName: "Api 文档",
                    url: "/openapi",
                    component: "IFrame",
                    description: "Api 文档",
                    multiTenancySides: MultiTenancySides.Host)
                // TODO: 注意在部署完毕之后手动修改此菜单iframe地址
                .SetProperty("frameSrc", "http://127.0.0.1:30000/swagger/index.html"));

            manage.AddItem(
                new ApplicationMenu(
                    name: "Caches",
                    displayName: "缓存管理",
                    url: "/manage/cache",
                    component: "/caching-management/cache/index",
                    description: "缓存管理"));

            return new NavigationDefinition(manage);
        }

        private static NavigationDefinition GetSaas()
        {
            var saas = new ApplicationMenu(
                name: "Saas",
                displayName: "Saas",
                url: "/saas",
                component: "",
                description: "Saas",
                icon: "ant-design:cloud-server-outlined",
                multiTenancySides: MultiTenancySides.Host);
            saas.AddItem(
              new ApplicationMenu(
                  name: "Tenants",
                  displayName: "租户管理",
                  url: "/saas/tenants",
                  component: "/saas/tenant/index",
                  description: "租户管理",
                  multiTenancySides: MultiTenancySides.Host));
            saas.AddItem(
              new ApplicationMenu(
                  name: "Editions",
                  displayName: "版本管理",
                  url: "/saas/editions",
                  component: "/saas/editions/index",
                  description: "版本管理",
                  multiTenancySides: MultiTenancySides.Host));

            return new NavigationDefinition(saas);
        }

        private static NavigationDefinition GetPlatform()
        {
            var platform = new ApplicationMenu(
                name: "Platform",
                displayName: "平台管理",
                url: "/platform",
                component: "",
                description: "平台管理",
                icon: "ep:platform");
            platform.AddItem(
              new ApplicationMenu(
                  name: "DataDictionary",
                  displayName: "数据字典",
                  url: "/platform/data-dic",
                  component: "/platform/dataDic/index",
                  description: "数据字典"));
            platform.AddItem(
              new ApplicationMenu(
                  name: "Layout",
                  displayName: "布局",
                  url: "/platform/layout",
                  component: "/platform/layout/index",
                  description: "布局"));
            platform.AddItem(
              new ApplicationMenu(
                  name: "Menu",
                  displayName: "菜单",
                  url: "/platform/menu",
                  component: "/platform/menu/index",
                  description: "菜单"));

            return new NavigationDefinition(platform);
        }

        private static NavigationDefinition GetLocalization()
        {
            var localization = new ApplicationMenu(
                name: "Localization",
                displayName: "本地化管理",
                url: "/localization",
                component: "",
                description: "本地化管理",
                icon: "ant-design:translation-outlined",
                multiTenancySides: MultiTenancySides.Host);
            localization.AddItem(
              new ApplicationMenu(
                  name: "Languages",
                  displayName: "语言管理",
                  url: "/localization/languages",
                  component: "/localization/languages/index",
                  description: "语言管理",
                  multiTenancySides: MultiTenancySides.Host)
                );
            localization.AddItem(
              new ApplicationMenu(
                  name: "Resources",
                  displayName: "资源管理",
                  url: "/localization/resources",
                  component: "/localization/resources/index",
                  description: "资源管理",
                  multiTenancySides: MultiTenancySides.Host)
                );
            localization.AddItem(
              new ApplicationMenu(
                  name: "Texts",
                  displayName: "文档管理",
                  url: "/localization/texts",
                  component: "/localization/texts/index",
                  description: "文档管理",
                  multiTenancySides: MultiTenancySides.Host)
                );

            return new NavigationDefinition(localization);
        }

    }
}
