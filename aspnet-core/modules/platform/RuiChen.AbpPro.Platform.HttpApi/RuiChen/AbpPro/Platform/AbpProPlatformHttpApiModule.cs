using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using RuiChen.AbpPro.Platform;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace Pure.Platform.HttpApi
{
    [DependsOn(
        typeof(AbpProPlatformApplicationContractModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class AbpProPlatformHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(options =>
            {
                options.AddApplicationPartIfNotExists(typeof(AbpProPlatformApplicationContractModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<FormOptions>(options =>
            {
                options.ValueLengthLimit = int.MaxValue;
                options.MultipartBodyLengthLimit = int.MaxValue;
                options.MultipartHeadersLengthLimit = int.MaxValue;
            });
        }
    }
}
