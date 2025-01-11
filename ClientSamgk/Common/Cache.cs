using System.Diagnostics.CodeAnalysis;
using ClientSamgk.Models;

namespace ClientSamgk.Common;

public sealed class Cache<T> where T : class
{
    private List<LifeTimeMemory<T>> _cache = [];

    public bool TryExtractFromCache(Func<T, bool> predicate, [MaybeNullWhen(false)] out T value)
    {
        var obj = ExtractFromCache(predicate);
        if (obj == null)
        {
            value = default;
            return false;
        }

        value = obj;
        return true;
    }

    public T? ExtractFromCache(Func<T, bool> predicate)
    {
        ClearCache();
        return _cache.FirstOrDefault(x => predicate(x.Object))?.Object;
    }

    public void SaveToCache(T item, int lifeTimeInMinutes)
    {
        var cacheItem = new LifeTimeMemory<T>
        {
            Object = item,
            DateTimeCanBeDeleted = DateTime.Now.AddMinutes(lifeTimeInMinutes),
            DateTimeAdded = DateTime.Now
        };
        _cache.Add(cacheItem);
    }

    public void ClearCache()
    {
        foreach (var item in _cache.Where(x => DateTime.Now >= x.DateTimeCanBeDeleted).ToList())
            _cache.Remove(item);
    }

    public bool IsCacheOutdated() =>
        _cache.Any(x => x.DateTimeCanBeDeleted <= DateTime.Now) || _cache.Count == 0;
}