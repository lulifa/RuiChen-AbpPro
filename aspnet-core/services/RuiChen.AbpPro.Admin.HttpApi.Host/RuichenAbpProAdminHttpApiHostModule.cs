using RuiChen.AbpPro.Account;
using RuiChen.AbpPro.Admin.EntityFrameworkCore;
using RuiChen.AbpPro.AspNetCore.HttpOverrides;
using RuiChen.AbpPro.AspNetCore.Mvc.Wrapper;
using RuiChen.AbpPro.Auditing;
using RuiChen.AbpPro.AuditLogging;
using RuiChen.AbpPro.CachingManagement;
using RuiChen.AbpPro.Data.DbMigrator;
using RuiChen.AbpPro.ExceptionHandling;
using RuiChen.AbpPro.FeatureManagement;
using RuiChen.AbpPro.HttpClient.Wrapper;
using RuiChen.AbpPro.Identity;
using RuiChen.AbpPro.LocalizationManagement;
using RuiChen.AbpPro.OpenIddict;
using RuiChen.AbpPro.PermissionManagement;
using RuiChen.AbpPro.Platform;
using RuiChen.AbpPro.Saas;
using RuiChen.AbpPro.Serilog.Enrichers.Application;
using RuiChen.AbpPro.SettingManagement;
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

namespace RuiChen.AbpPro.Admin.HttpApi.Host
{
    [DependsOn(

        typeof(AbpProAccountHttpApiModule),
        typeof(AbpProAccountApplicationModule),
        typeof(AbpAccountWebOpenIddictModule),

        typeof(AbpProAuditingHttpApiModule),
        typeof(AbpProAuditingApplicationModule),
        typeof(AbpProAuditLoggingEntityFrameworkCoreModule),


        typeof(AbpProCachingManagementHttpApiModule),
        typeof(AbpProCachingManagementApplicationModule),
        typeof(AbpProCachingManagementStackExchangeRedisModule),


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

        typeof(AbpProPlatformHttpApiModule),
        typeof(AbpProPlatformApplicationModule),
        typeof(AbpProPlatformEntityFrameworkCoreModule),


        typeof(AbpProSaasHttpApiModule),
        typeof(AbpProSaasApplicationModule),
        typeof(AbpProSaasEntityFrameworkCoreModule),
        typeof(AbpAspNetCoreMultiTenancyModule),

        typeof(AbpProSettingManagementHttpApiModule),
        typeof(AbpProSettingManagementApplicationModule),
        typeof(AbpSettingManagementEntityFrameworkCoreModule),



        typeof(AbpAspNetCoreMvcWrapperModule),
        typeof(AbpHttpClientWrapperModule),
        typeof(AbpAspNetCoreHttpOverridesModule),


        typeof(AbpAspNetCoreSerilogModule),
        typeof(AbpProSerilogEnrichersApplicationModule),


        typeof(AbpProExceptionHandlingModule),

        //typeof(AbpProDataDbMigratorModule),
        //typeof(RuiChenAbpProAdminMigrationEntityFrameworkCoreModule),

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

            ConfigureWrapper();
            ConfigureDbContext();
            ConfigureLocalization();
            ConfigureKestrelServer();
            ConfigureExceptionHandling();
            ConfigureVirtualFileSystem();
            ConfigureUrls(configuration);
            ConfigureCaching(configuration);
            ConfigureAuditing(configuration);
            ConfigureIdentity(configuration);
            ConfigureAuthServer(configuration);
            ConfigureSwagger(services);
            ConfigureEndpoints(services);
            ConfigureMultiTenancy(configuration);
            ConfigureJsonSerializer(configuration);
            ConfigureFeatureManagement(configuration);
            ConfigureSettingManagement(configuration);
            ConfigurePermissionManagement(configuration);
            ConfigureCors(services, configuration);
            ConfigureDistributedLock(services, configuration);
            ConfigureSecurity(services, configuration, hostingEnvironment.IsDevelopment());
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var configuration = context.GetConfiguration();

            app.UseForwardedHeaders();
            app.UseCookiePolicy();
            app.UseMapRequestLocalization();
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
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "RuiChenAdmin API");
            });
            // 审计日志
            app.UseAuditing();
            app.UseAbpSerilogEnrichers();
            // 路由
            app.UseConfiguredEndpoints();
        }

    }
}
