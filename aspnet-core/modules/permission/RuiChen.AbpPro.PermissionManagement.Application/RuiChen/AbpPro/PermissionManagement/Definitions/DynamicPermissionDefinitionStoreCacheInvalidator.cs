using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Threading;
using Volo.Abp.Timing;

namespace RuiChen.AbpPro.PermissionManagement
{
    public class DynamicPermissionDefinitionStoreCacheInvalidator :
        ILocalEventHandler<EntityChangedEventData<PermissionGroupDefinitionRecord>>,
        ILocalEventHandler<EntityChangedEventData<PermissionDefinitionRecord>>,
        ITransientDependency
    {
        private readonly IClock clock;
        private readonly IDistributedCache distributedCache;
        private readonly IDynamicPermissionDefinitionStoreInMemoryCache storeCache;
        private readonly AbpDistributedCacheOptions cacheOptions;

        public DynamicPermissionDefinitionStoreCacheInvalidator(
            IClock clock,
            IDistributedCache distributedCache,
            IDynamicPermissionDefinitionStoreInMemoryCache storeCache,
            IOptions<AbpDistributedCacheOptions> cacheOptions)
        {
            this.clock = clock;
            this.distributedCache = distributedCache;
            this.storeCache = storeCache;
            this.cacheOptions = cacheOptions.Value;
        }

        public async virtual Task HandleEventAsync(EntityChangedEventData<PermissionGroupDefinitionRecord> eventData)
        {
            await RemoveStampInDistributedCacheAsync();
        }

        public async virtual Task HandleEventAsync(EntityChangedEventData<PermissionDefinitionRecord> eventData)
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
            return $"{cacheOptions.KeyPrefix}_AbpInMemoryPermissionCacheStamp";
        }

    }
}
