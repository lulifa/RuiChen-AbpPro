using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.Identity.AspNetCore;
using Volo.Abp.Identity.Localization;
using Volo.Abp.Modularity;
using VoloAbpIdentityApplicationContractsModule = Volo.Abp.Identity.AbpIdentityApplicationContractsModule;
using VoloAbpIdentityHttpApiModule = Volo.Abp.Identity.AbpIdentityHttpApiModule;

namespace RuiChen.AbpPro.Identity
{
    [DependsOn(
        typeof(VoloAbpIdentityHttpApiModule),
        typeof(AbpProIdentityApplicationContractsModule),
        typeof(AbpIdentityAspNetCoreModule)
        )]
    public class AbpProIdentityHttpApiModule : AbpModule
    {
        /// <summary>
        /// 本地化资源：将相关的本地化资源程序集添加到 MVC 数据注解本地化选项中，以便在数据注解验证中使用这些资源。
        /// MVC 配置：确保模块中的控制器和组件被正确地添加到 MVC 构建器中，使它们在应用程序中可用。
        /// </summary>
        /// <param name="context"></param>
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(typeof(IdentityResource), typeof(AbpProIdentityApplicationContractsModule).Assembly);
                options.AddAssemblyResource(typeof(IdentityResource), typeof(VoloAbpIdentityApplicationContractsModule).Assembly);
            });

            PreConfigure<IMvcBuilder>(options =>
            {
                options.AddApplicationPartIfNotExists(typeof(AbpProIdentityHttpApiModule).Assembly);
            });

        }
    }
}
