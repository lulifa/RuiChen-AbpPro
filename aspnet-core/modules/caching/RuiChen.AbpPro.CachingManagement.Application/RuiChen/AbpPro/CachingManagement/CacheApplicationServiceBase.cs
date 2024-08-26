using Volo.Abp.Application.Services;

namespace RuiChen.AbpPro.CachingManagement
{
    public abstract class CacheApplicationServiceBase : ApplicationService
    {
        protected CacheApplicationServiceBase()
        {
            LocalizationResource = typeof(CacheResource);
        }
    }
}
