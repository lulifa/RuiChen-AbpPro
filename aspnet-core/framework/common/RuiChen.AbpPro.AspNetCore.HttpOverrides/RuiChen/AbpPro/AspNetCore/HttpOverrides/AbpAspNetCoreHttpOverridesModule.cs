using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RuiChen.AbpPro.AspNetCore.WebClientInfo;
using Volo.Abp.AspNetCore;
using Volo.Abp.AspNetCore.WebClientInfo;
using Volo.Abp.Modularity;

namespace RuiChen.AbpPro.AspNetCore.HttpOverrides
{
    [DependsOn(
        typeof(AbpAspNetCoreModule)
        )]
    public class AbpAspNetCoreHttpOverridesModule : AbpModule
    {
        /// <summary>
        /// 在这个枚举 ForwardedHeaders 中，5 表示的是 XForwardedFor 和 XForwardedProto 的组合。
        /// 这是因为 5 在二进制中表示为 0101，而枚举值是通过位运算来组合的：
        /// XForwardedFor = 1 << 0，即 1，在二进制中是 0001。
        /// XForwardedProto = 1 << 2，即 4，在二进制中是 0100
        /// 将这两个值按位或（|）操作 变为0101
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            Configure<ForwardedHeadersOptions>(options =>
            {
                configuration.GetSection("Forwarded").Bind(options);
            });

            context.Services.Replace(ServiceDescriptor.Transient<IWebClientInfoProvider, RequestForwardedHeaderWebClientInfoProvider>());
        }
    }
}
