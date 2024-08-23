namespace RuiChen.AbpPro.CachingManagement
{
    public interface ICacheManager
    {
        Task<CackeKeysResponse> GetKeysAsync(GetCacheKeysRequest request, CancellationToken cancellationToken = default);

        Task<CacheValueResponse> GetValueAsync(string key, CancellationToken cancellationToken = default);

        Task SetAsync(SetCacheRequest request, CancellationToken cancellationToken = default);

        Task RefreshAsync(RefreshCacheRequest request, CancellationToken cancellationToken = default);

        Task RemoveAsync(string key, CancellationToken cancellationToken = default);
    }
}
