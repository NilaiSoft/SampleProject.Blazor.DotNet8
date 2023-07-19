using Microsoft.Extensions.Caching.Memory;
using SampleProject.Core;
using System.Collections;

namespace SampleProject.Server.Services
{
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
    }
}
