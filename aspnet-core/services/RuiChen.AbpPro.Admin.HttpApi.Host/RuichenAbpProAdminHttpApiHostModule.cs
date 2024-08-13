using Microsoft.AspNetCore.Builder;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.OpenIddict;
using RuiChen.AbpPro.OpenIddict;

namespace RuiChen.AbpPro.Admin.HttpApi.Host
{
    [DependsOn(

        //认证服务器模块
        typeof(AbpProOpenIddictHttpApiModule),
        //typeof(AbpProOpenIddictApplicationModule),
        //typeof(AbpOpenIddictDomainModule),
        //typeof(AbpOpenIddictEntityFrameworkCoreModule),

        typeof(AbpAutofacModule)
        )]
    public partial class RuichenAbpProAdminHttpApiHostModule : AbpModule
    {

        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            base.PreConfigureServices(context);
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var services = context.Services;
            var hostingEnvironment = services.GetHostingEnvironment();
            var configuration = services.GetConfiguration();

            ConfigureAuditing();
            ConfigureDbContext();
            ConfigureKestrelServer();
            ConfigureAuthServer(configuration);
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
