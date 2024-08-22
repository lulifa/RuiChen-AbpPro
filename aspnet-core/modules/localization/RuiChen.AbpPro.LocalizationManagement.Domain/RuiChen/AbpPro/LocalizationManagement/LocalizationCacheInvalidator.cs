using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Threading;
using Volo.Abp.Timing;

namespace RuiChen.AbpPro.LocalizationManagement
{
    public class LocalizationCacheInvalidator :
    ILocalEventHandler<EntityChangedEventData<Language>>,
    ILocalEventHandler<EntityChangedEventData<Resource>>,
    ILocalEventHandler<EntityChangedEventData<LanguageText>>,

    IDistributedEventHandler<EntityCreatedEto<LanguageTextEto>>,
    IDistributedEventHandler<EntityUpdatedEto<LanguageTextEto>>,
    IDistributedEventHandler<EntityDeletedEto<LanguageTextEto>>,

    IDistributedEventHandler<EntityCreatedEto<ResourceEto>>,
    IDistributedEventHandler<EntityUpdatedEto<ResourceEto>>,
    IDistributedEventHandler<EntityDeletedEto<ResourceEto>>,

    IDistributedEventHandler<EntityCreatedEto<LanguageEto>>,
    IDistributedEventHandler<EntityUpdatedEto<LanguageEto>>,
    IDistributedEventHandler<EntityDeletedEto<LanguageEto>>,

    ITransientDependency
    {
        private readonly IClock _clock;
        private readonly IDistributedCache _distributedCache;
        private readonly ILocalizationStoreCache _storeCache;
        private readonly AbpDistributedCacheOptions _distributedCacheOptions;

        public LocalizationCacheInvalidator(
            IClock clock,
            ILocalizationStoreCache storeCache,
            IDistributedCache distributedCache,
            IOptions<AbpDistributedCacheOptions> options)
        {
            _clock = clock;
            _storeCache = storeCache;
            _distributedCache = distributedCache;
            _distributedCacheOptions = options.Value;
        }

        public async virtual Task HandleEventAsync(EntityChangedEventData<Language> eventData)
        {
            await RemoveStampInDistributedCacheAsync();
        }

        public async virtual Task HandleEventAsync(EntityChangedEventData<Resource> eventData)
        {
            await RemoveStampInDistributedCacheAsync();
        }

        public async virtual Task HandleEventAsync(EntityChangedEventData<LanguageText> eventData)
        {
            await RemoveStampInDistributedCacheAsync();
        }

        public async virtual Task HandleEventAsync(EntityCreatedEto<LanguageTextEto> eventData)
        {
            await RemoveStampInDistributedCacheAsync();
        }

        public async virtual Task HandleEventAsync(EntityUpdatedEto<LanguageTextEto> eventData)
        {
            await RemoveStampInDistributedCacheAsync();
        }

        public async virtual Task HandleEventAsync(EntityDeletedEto<LanguageTextEto> eventData)
        {
            await RemoveStampInDistributedCacheAsync();
        }

        public async virtual Task HandleEventAsync(EntityCreatedEto<ResourceEto> eventData)
        {
            await RemoveStampInDistributedCacheAsync();
        }

        public async virtual Task HandleEventAsync(EntityDeletedEto<ResourceEto> eventData)
        {
            await RemoveStampInDistributedCacheAsync();
        }

        public async virtual Task HandleEventAsync(EntityCreatedEto<LanguageEto> eventData)
        {
            await RemoveStampInDistributedCacheAsync();
        }

        public async virtual Task HandleEventAsync(EntityUpdatedEto<LanguageEto> eventData)
        {
            await RemoveStampInDistributedCacheAsync();
        }

        public async virtual Task HandleEventAsync(EntityDeletedEto<LanguageEto> eventData)
        {
            await RemoveStampInDistributedCacheAsync();
        }

        public async virtual Task HandleEventAsync(EntityUpdatedEto<ResourceEto> eventData)
        {
            await RemoveStampInDistributedCacheAsync();
        }

        protected async virtual Task RemoveStampInDistributedCacheAsync()
        {
            using (await _storeCache.SyncSemaphore.LockAsync())
            {
                var cacheKey = $"{_distributedCacheOptions.KeyPrefix}_AbpInMemoryLocalizationCacheStamp";

                await _distributedCache.RemoveAsync(cacheKey);

                _storeCache.CacheStamp = Guid.NewGuid().ToString();
                _storeCache.LastCheckTime = _clock.Now.AddMinutes(-5);
            }
        }
    }
}
