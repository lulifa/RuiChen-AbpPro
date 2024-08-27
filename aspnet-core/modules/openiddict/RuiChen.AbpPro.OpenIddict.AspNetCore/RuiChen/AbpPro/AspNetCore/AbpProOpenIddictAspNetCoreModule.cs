using Microsoft.Extensions.DependencyInjection;
using RuiChen.AbpPro.Identity;
using Volo.Abp.Modularity;
using VoloAbpOpenIddictAspNetCoreModule = Volo.Abp.OpenIddict.AbpOpenIddictAspNetCoreModule;

namespace RuiChen.AbpPro.OpenIddict.AspNetCore
{
    [DependsOn(
        typeof(VoloAbpOpenIddictAspNetCoreModule),
        typeof(AbpProIdentityDomainSharedModule)
        )]
    public class AbpProOpenIddictAspNetCoreModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<OpenIddictServerBuilder>(builder =>
            {
                builder.RegisterClaims([IdentityConsts.ClaimType.Avatar.Name]);
            });
        }
    }
}
