using Volo.Abp.Modularity;
using VoloAbpIdentityDomainModule = Volo.Abp.Identity.AbpIdentityDomainModule;

namespace RuiChen.AbpPro.Identity
{
    [DependsOn(
        typeof(AbpProIdentityDomainSharedModule),
        typeof(VoloAbpIdentityDomainModule)
        )]
    public class AbpProIdentityDomainModule : AbpModule
    {
    }
}
