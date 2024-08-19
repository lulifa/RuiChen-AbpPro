using Localization.Resources.AbpUi;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict.Localization;

namespace RuiChen.AbpPro.OpenIddict
{
    [DependsOn(
        typeof(AbpProOpenIddictApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule)
        )]
    public class AbpProOpenIddictHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            // 这段代码设置了 MVC 数据注释的本地化选项。如果你的 API 使用了数据注释进行模型验证，
            // 那么本地化这些验证错误信息将会是必要的。这段配置确保数据注释的本地化资源可以从你的模块中获取。
            PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(typeof(AbpOpenIddictResource), typeof(AbpProOpenIddictApplicationContractsModule).Assembly);
            });
               
            // 这段代码确保 API 模块的程序集被添加到 MVC 构建过程中。
            // 这对于让 ASP.NET Core 能够识别并使用模块中的控制器非常重要。即使你只编写了 API 控制器
            // 也需要确保这些控制器可以被 MVC 框架找到并正确处理
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(AbpProOpenIddictHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            // 这段代码配置了 ABP 框架的本地化选项。AbpOpenIddictResource 是一个本地化资源类，
            // 它可能包含与 OpenIddict 相关的本地化字符串。
            // AddBaseTypes方法将 AbpUiResource作为AbpOpenIddictResource 的基类型，
            // 允许 AbpOpenIddictResource 继承 AbpUiResource 的本地化字符串。
            // 这种配置使得 AbpOpenIddictResource 可以重用 AbpUiResource 中定义的本地化资源，从而简化本地化的管理
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources.Get<AbpOpenIddictResource>().AddBaseTypes(typeof(AbpUiResource));
            });
        }

    }
}
