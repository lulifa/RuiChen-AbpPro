using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RuiChen.AbpPro.Wrapper;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace RuiChen.AbpPro.HttpClient.Wrapper
{
    [DependsOn(
        typeof(AbpHttpClientModule),
        typeof(AbpWrapperModule)
        )]
    public class AbpHttpClientWrapperModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<AbpHttpClientBuilderOptions>(options =>
            {
                options.ProxyClientBuildActions.Add((_, builder) =>
                {
                    builder.ConfigureHttpClient((provider, client) =>
                    {
                        var wrapperOptions = provider.GetRequiredService<IOptions<AbpWrapperOptions>>().Value;
                        var wrapperHeader = wrapperOptions.IsEnabled
                            ? AbpHttpWrapConsts.AbpWrapResult
                            : AbpHttpWrapConsts.AbpDontWrapResult;

                        client.DefaultRequestHeaders.TryAddWithoutValidation(wrapperHeader, "true");
                    });
                });
            });
        }
    }
}
