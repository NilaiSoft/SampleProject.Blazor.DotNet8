using SampleProject.Core;

namespace SampleProject.Server.Services
{
    public interface ICacheManager<T>
    {
        Task<IPagedList<T>> GetAsync<T>(string key, Func<Task<IPagedList<T>>> acquire);
        Task<IList<T>> GetAsync<T>(string key, Func<Task<IList<T>>> acquire);
        string GetCacheName(string key);
        IList<string> GetCacheNamesByPrefixEntityName(string entityName);
        void RemoveRangeByPrefixEntityName(string entityName);
        void Remove(string v);
    }
}