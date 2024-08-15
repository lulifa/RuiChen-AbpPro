using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using VoloAbpIdentityApplicationModule = Volo.Abp.Identity.AbpIdentityApplicationModule;

namespace RuiChen.AbpPro.Identity
{
    [DependsOn(
        typeof(VoloAbpIdentityApplicationModule),
        typeof(AbpProIdentityApplicationContractsModule),
        typeof(AbpProIdentityDomainModule)
        )]
    public class AbpProIdentityApplicationModule : AbpModule
    {
        /// <summary>
        /// 注册 AutoMapper 服务：将 AutoMapper 服务添加到服务容器中，以便在应用程序中使用。
        /// 自动配置 AutoMapper：根据指定模块的配置文件自动设置映射规则，使得对象之间的映射变得简单和一致。
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ServiceConfigurationContext context)
        {

            context.Services.AddAutoMapperObjectMapper<AbpProIdentityApplicationModule>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<AbpProIdentityApplicationModuleAutoMapperProfile>(validate: true);
            });

        }
    }
}
