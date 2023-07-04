using SampleProject.Core;

namespace SampleProject.Server.Services
{
    public interface ICacheManager<T>
    {
        Task<IPagedList<T>> GetAsync<T>(string key, Func<Task<IPagedList<T>>> acquire);
    }
}