using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Threading;
using Volo.Abp.Timing;

namespace RuiChen.AbpPro.FeatureManagement;
public class DynamicPermissionDefinitionStoreCacheInvalidator :
    ILocalEventHandler<EntityChangedEventData<FeatureGroupDefinitionRecord>>,
    ILocalEventHandler<EntityChangedEventData<FeatureDefinitionRecord>>,
    ITransientDependency
{
    private readonly IDynamicFeatureDefinitionStoreInMemoryCache storeCache;

    private readonly IClock clock;
    private readonly IDistributedCache distributedCache;
    private readonly AbpDistributedCacheOptions cacheOptions;

    public DynamicPermissionDefinitionStoreCacheInvalidator(
        IClock clock,
        IDistributedCache distributedCache,
        IDynamicFeatureDefinitionStoreInMemoryCache storeCache,
        IOptions<AbpDistributedCacheOptions> cacheOptions)
    {
        this.storeCache = storeCache;
        this.clock = clock;
        this.distributedCache = distributedCache;
        this.cacheOptions = cacheOptions.Value;
    }

    public async virtual Task HandleEventAsync(EntityChangedEventData<FeatureGroupDefinitionRecord> eventData)
    {
        await RemoveStampInDistributedCacheAsync();
    }

    public async virtual Task HandleEventAsync(EntityChangedEventData<FeatureDefinitionRecord> eventData)
    {
        await RemoveStampInDistributedCacheAsync();
    }

    protected async virtual Task RemoveStampInDistributedCacheAsync()
    {
        using (await storeCache.SyncSemaphore.LockAsync())
        {
            var cacheKey = GetCommonStampCacheKey();

            await distributedCache.RemoveAsync(cacheKey);

            storeCache.CacheStamp = Guid.NewGuid().ToString();
            storeCache.LastCheckTime = clock.Now.AddMinutes(-5);
        }
    }

    protected virtual string GetCommonStampCacheKey()
    {
        return $"{cacheOptions.KeyPrefix}_AbpInMemoryFeatureCacheStamp";
    }
}
