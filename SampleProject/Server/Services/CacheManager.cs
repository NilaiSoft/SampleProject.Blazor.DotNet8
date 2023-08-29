namespace SampleProject.Server.Services;
public class CacheManager<T> : ICacheManager<T>
{
    private readonly IMemoryCache _memoryCache;

    public CacheManager(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public async Task<IPagedList<T>> GetAsync<T>(string key, Func<Task<IPagedList<T>>> acquire)
    {
        if (_memoryCache.TryGetValue(key, out IPagedList<T>? result))
        {
            return result;
        }

        result = await acquire();

        if (result != null)
        {
            _memoryCache.Set(key, result);
        }
        return result;
    }

    public string GetCacheName(string key)
    {
        var coherentState = typeof(MemoryCache).GetField("_coherentState", BindingFlags.NonPublic | BindingFlags.Instance);
        var coherentStateValue = coherentState?.GetValue(_memoryCache);
        var entriesCollection = coherentStateValue?.GetType().GetProperty("EntriesCollection", BindingFlags.NonPublic | BindingFlags.Instance);
        var entriesCollectionValue = entriesCollection?.GetValue(coherentStateValue) as ICollection;
        var tets = entriesCollectionValue?.GetType().GetProperties().ToList();
        var keys = new List<string>();
        if (entriesCollectionValue != null)
        {
            foreach (var item in entriesCollectionValue)
            {
                var methodInfo = item.GetType().GetProperty("Key");

                var val = methodInfo?.GetValue(item);

                keys?.Add(val?.ToString());
            }
        }

        return keys.FirstOrDefault(x => x.StartsWith(key)) ?? "";
    }

    public IList<string> GetCacheNamesByPrefixEntityName(string entityName)
    {
        entityName = entityName.ToLower();

        var coherentState = typeof(MemoryCache).GetField("_coherentState", BindingFlags.NonPublic | BindingFlags.Instance);
        var coherentStateValue = coherentState?.GetValue(_memoryCache);
        var entriesCollection = coherentStateValue?.GetType().GetProperty("EntriesCollection", BindingFlags.NonPublic | BindingFlags.Instance);
        var entriesCollectionValue = entriesCollection?.GetValue(coherentStateValue) as ICollection;
        var tets = entriesCollectionValue?.GetType().GetProperties().ToList();
        var keys = new List<string>();
        if (entriesCollectionValue != null)
        {
            foreach (var item in entriesCollectionValue)
            {
                var methodInfo = item.GetType().GetProperty("Key");

                var val = methodInfo?.GetValue(item);

                keys?.Add(val?.ToString());
            }
        }

        return keys.Where(x => x.StartsWith(entityName)).ToList() ?? new List<string>();
    }

    public async Task<IList<T>> GetAsync<T>(string key, Func<Task<IList<T>>> acquire)
    {
        if (_memoryCache.TryGetValue(key, out IList<T>? result))
            return result;

        result = await acquire();

        if (result != null)
        {
            _memoryCache.Set(key, result);
        }

        return result;
    }

    public void Remove(string key)
    {
        _memoryCache.Remove(key);
    }

    public void RemoveRangeByPrefixEntityName(string entityName)
    {
        entityName = $"{entityName.Trim()}-";
        var keys = GetCacheNamesByPrefixEntityName(entityName);

        foreach (var key in keys)
        {
            _memoryCache.Remove(key);
        }
    }
}
