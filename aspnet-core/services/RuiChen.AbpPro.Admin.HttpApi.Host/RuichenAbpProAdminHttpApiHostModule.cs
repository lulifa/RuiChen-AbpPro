using RuiChen.AbpPro.Account;
using RuiChen.AbpPro.Identity;
using RuiChen.AbpPro.OpenIddict;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Identity.AspNetCore;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.HttpApi;
using Volo.Abp.SettingManagement;

namespace RuiChen.AbpPro.Admin.HttpApi.Host
{
    [DependsOn(

        typeof(AbpProAccountHttpApiModule),
        typeof(AbpProAccountApplicationModule),
        typeof(AbpProAccountTemplatesModule),
        //typeof(AbpAccountWebOpenIddictModule),
        //typeof(AbpAspNetCoreMvcUiBasicThemeModule),

        typeof(AbpProIdentityHttpApiModule),
        typeof(AbpProIdentityApplicationModule),
        typeof(AbpProIdentityEntityFrameworkCoreModule),
        typeof(AbpProIdentityOrganizaztionUnitsModule),

        typeof(AbpProOpenIddictHttpApiModule),
        typeof(AbpProOpenIddictApplicationModule),
        typeof(AbpOpenIddictEntityFrameworkCoreModule),

        typeof(AbpPermissionManagementHttpApiModule),
        typeof(AbpPermissionManagementApplicationModule),
        typeof(AbpPermissionManagementDomainModule),
        typeof(AbpPermissionManagementEntityFrameworkCoreModule),
        typeof(AbpAspNetCoreMultiTenancyModule),

        typeof(AbpAspNetCoreSerilogModule),

        typeof(AbpAutofacModule)
        )]
    public partial class RuichenAbpProAdminHttpApiHostModule : AbpModule
    {

        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

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
            ConfigureIdentity(configuration);
            ConfigureAuthServer(configuration);
            ConfigureSwagger(services);
            ConfigureEndpoints(services);
            ConfigureMultiTenancy(configuration);
            ConfigureJsonSerializer(configuration);
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
