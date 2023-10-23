using IdentityModel;
using SampleProject.Server.Data;
using SampleProject.Server.VModels;
using System.Linq.Expressions;

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

        public override async Task<IList<NavMenu>> GetAllAsync(Expression<Func<NavMenu, bool>> _pridicate)
        {
            async Task<IList<NavMenu>> getAllAsync()
            {
                var query = _dbSet.Where(_pridicate).AsQueryable();
                return await AddDeletedFilter(query, false)
                    .OrderBy(x => x.Ordering)
                    .ToListAsync();
            }

            return await getAllAsync();
        }

        public async Task<int> AddProductAsync(NavMenu product)
        {
            product = (await _entityRepository.AddAsync(product)).Entity;

            await _entityRepository.SaveChangesAsync();

            return product.Id;
        }
    }
}
