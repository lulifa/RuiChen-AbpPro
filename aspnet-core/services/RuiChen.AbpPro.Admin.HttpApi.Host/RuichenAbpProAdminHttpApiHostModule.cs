using Microsoft.AspNetCore.Builder;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.OpenIddict;
using RuiChen.AbpPro.OpenIddict;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Identity.AspNetCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.Identity;
using RuiChen.AbpPro.Identity;

namespace RuiChen.AbpPro.Admin.HttpApi.Host
{
    [DependsOn(

        //认证模块
        typeof(AbpIdentityAspNetCoreModule),
        typeof(AbpProIdentityHttpApiModule),
        typeof(AbpProIdentityApplicationModule),
        typeof(AbpProIdentityDomainModule),
        typeof(AbpProIdentityEntityFrameworkCoreModule),
        //typeof(AbpIdentityOrganizaztionUnitsModule),

        //认证服务器模块
        typeof(AbpProOpenIddictHttpApiModule),
        typeof(AbpProOpenIddictApplicationModule),
        typeof(AbpOpenIddictEntityFrameworkCoreModule),

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
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Admin API");
            });
            // 审计日志
            app.UseAuditing();
            app.UseAbpSerilogEnrichers();
            // 路由
            app.UseConfiguredEndpoints();
        }

    }
}
