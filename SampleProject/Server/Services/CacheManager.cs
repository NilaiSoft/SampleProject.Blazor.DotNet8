using Microsoft.Extensions.Caching.Memory;
using Mono.TextTemplating;
using SampleProject.Core;
using SampleProject.Shared.Dtos.Product;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                return result;

            result = await acquire();

            if (result != null)
            {
                _memoryCache.Set(key, result);
            }
            return result;
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

    }
}
