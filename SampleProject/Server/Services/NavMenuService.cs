using IdentityModel;
using SampleProject.Server.Data;
using SampleProject.Server.VModels;

namespace SampleProjects.Server.Services
{
    public class NavMenuService : EntityRepository<NavMenu, NavMenuModel>, INavMenuService
    {
        readonly IEntityRepository<NavMenu, NavMenuModel> _entityRepository;
        public NavMenuService(ApplicationDbContext dbContext
            , IEntityRepository<NavMenu, NavMenuModel> entityRepository)
            : base(dbContext)
        {
            _entityRepository = entityRepository;
        }

        public async Task<int> AddProductAsync(NavMenu product)
        {
            product = (await _entityRepository.AddAsync(product)).Entity;

            await _entityRepository.SaveChangesAsync();

            return product.Id;
        }
    }
}
