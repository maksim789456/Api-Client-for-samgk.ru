using ClientSamgk.Interfaces;
using ClientSamgk.Models.Params.Implementations.Cache;

namespace ClientSamgk.Cache;

public sealed class CacheManager<T> where T : class
{
    public IReadOnlyList<LifeTimeMemory<T>> Data => Cache.Data;
    public readonly ICache<T> Cache;

    private readonly IDataFetcher<T> _fetcher;
    private readonly CacheOptions _cacheOptions;

    public CacheManager(ICache<T> cache, IDataFetcher<T> fetcher, CacheOptions cacheOptions)
    {
        Cache = cache;
        _fetcher = fetcher;
        _cacheOptions = cacheOptions;
    }

    public async Task EnsureCacheAsync(CancellationToken cToken = default)
    {
        if (!Cache.IsCacheOutdated()) return;

        var data = await _fetcher.FetchAsync(cToken);

        Cache.DropCache();

        foreach (var item in data)
            Cache.SaveToCache(item, _cacheOptions.LifeTimeObjectsForCommon);
    }
}