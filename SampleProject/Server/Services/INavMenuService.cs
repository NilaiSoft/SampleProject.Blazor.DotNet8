using SampleProject.Server.Data;
using SampleProject.Server.VModels;

namespace SampleProjects.Server.Services
{
    public interface INavMenuService : IEntityRepository<NavMenu, NavMenuModel>
    {
    }
}
