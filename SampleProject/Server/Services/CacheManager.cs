using Microsoft.Extensions.Caching.Memory;
using Mono.TextTemplating;
using SampleProject.Core;
using SampleProject.Shared.Dtos.Product;

namespace SampleProject.Server.Services
{
    public class CacheManager<T> : ICacheManager<T>
    {
        private readonly IMemoryCache _memoryCache;

        public CacheManager(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public void Get(string key, Func<IPagedList<T>> acquire, out IPagedList<T> objects)
        {
            if (_memoryCache.TryGetValue(key, out objects))
            {
                // Data successfully read from cache
                // use myValue
            }
            else
            {
                objects = acquire();
                _memoryCache.Set(key, objects, TimeSpan.FromMinutes(10));

                _memoryCache.TryGetValue(key, out objects);
            }
        }
    }
}
