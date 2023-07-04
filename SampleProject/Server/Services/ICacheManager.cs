using SampleProject.Core;

namespace SampleProject.Server.Services
{
    public interface ICacheManager<T>
    {
        void Get(string key, Func<IPagedList<T>> acquire, out IPagedList<T> objects);
    }
}