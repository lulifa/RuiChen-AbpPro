using Volo.Abp.Caching;
using Volo.Abp.Modularity;

namespace RuiChen.AbpPro.CachingManagement
{
    [DependsOn(
        typeof(AbpCachingModule)
        )]
    public class AbpProCachingManagementDomainModule : AbpModule
    {
    }
}
