using RuiChen.AbpPro.Account;
using RuiChen.AbpPro.FeatureManagement;
using RuiChen.AbpPro.Identity;
using RuiChen.AbpPro.OpenIddict;
using RuiChen.AbpPro.PermissionManagement;
using RuiChen.AbpPro.Saas;
using Volo.Abp;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement;
using RuiChen.AbpPro.SettingManagement;
using RuiChen.AbpPro.LocalizationManagement;

namespace RuiChen.AbpPro.Admin.HttpApi.Host
{
    [DependsOn(

        typeof(AbpProAccountHttpApiModule),
        typeof(AbpProAccountApplicationModule),
        typeof(AbpAccountWebOpenIddictModule),

        typeof(AbpProFeatureManagementHttpApiModule),
        typeof(AbpProFeatureManagementApplicationModule),
        typeof(AbpFeatureManagementEntityFrameworkCoreModule),

        typeof(AbpProIdentityHttpApiModule),
        typeof(AbpProIdentityApplicationModule),
        typeof(AbpProIdentityEntityFrameworkCoreModule),
        typeof(AbpProIdentityOrganizaztionUnitsModule),

        typeof(AbpProLocalizationManagementHttpApiModule),
        typeof(AbpProLocalizationManagementApplicationModule),
        typeof(AbpProLocalizationManagementEntityFrameworkCoreModule),

        typeof(AbpProOpenIddictHttpApiModule),
        typeof(AbpProOpenIddictApplicationModule),
        typeof(AbpOpenIddictEntityFrameworkCoreModule),

        typeof(AbpProPermissionManagementHttpApiModule),
        typeof(AbpProPermissionManagementApplicationModule),
        typeof(AbpPermissionManagementEntityFrameworkCoreModule),
        typeof(AbpProPermissionManagementDomainOrganizationUnitsModule), // 组织机构权限管理


        typeof(AbpProSaasHttpApiModule),
        typeof(AbpProSaasApplicationModule),
        typeof(AbpProSaasEntityFrameworkCoreModule),
        typeof(AbpAspNetCoreMultiTenancyModule),

        typeof(AbpProSettingManagementHttpApiModule),
        typeof(AbpProSettingManagementApplicationModule),
        typeof(AbpSettingManagementEntityFrameworkCoreModule),


        typeof(AbpAspNetCoreSerilogModule),

        typeof(AbpAutofacModule)
        )]
    public partial class RuichenAbpProAdminHttpApiHostModule : AbpModule
    {

        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            PreConfigureFeature();
            PreConfigureIdentity();
            PreConfigureAuthServer(configuration);
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var services = context.Services;
            var hostingEnvironment = services.GetHostingEnvironment();
            var configuration = services.GetConfiguration();

            ConfigureAuditing();
            ConfigureDbContext();
            ConfigureKestrelServer();
            ConfigureVirtualFileSystem();
            ConfigureUrls(configuration);
            ConfigureIdentity(configuration);
            ConfigureAuthServer(configuration);
            ConfigureSwagger(services);
            ConfigureEndpoints(services);
            ConfigureMultiTenancy(configuration);
            ConfigureJsonSerializer(configuration);
            ConfigurePermissionManagement(configuration);
            ConfigureDistributedLock(services, configuration);
            ConfigureSecurity(services, configuration, hostingEnvironment.IsDevelopment());
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var configuration = context.GetConfiguration();

            app.UseForwardedHeaders();
            app.UseCookiePolicy();
            //app.UseMapRequestLocalization();
            // http调用链
            app.UseCorrelationId();
            // 虚拟文件系统
            app.UseStaticFiles();
            // 路由
            app.UseRouting();
            // 跨域
            app.UseCors();
            // 认证
            app.UseAuthentication();
            app.UseAbpClaimsMap();
            app.UseDynamicClaims();
            app.UseAbpOpenIddictValidation();
            // 多租户
            app.UseMultiTenancy();
            // 授权
            app.UseAuthorization();
            // Swagger
            app.UseSwagger();
            // Swagger可视化界面
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "RuiChenAbpProAdmin API");
            });
            // 审计日志
            app.UseAuditing();
            app.UseAbpSerilogEnrichers();
            // 路由
            app.UseConfiguredEndpoints();
        }

    }
}
