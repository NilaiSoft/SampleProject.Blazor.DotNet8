using IdentityModel;
using SampleProject.Server.Data;
using SampleProject.Server.VModels;

namespace SampleProjects.Server.Services
{
    public class ProductService : EntityRepository<Product, ProductModel>, IProductService
    {
        readonly IEntityRepository<Product, ProductModel> _entityRepository;
        public ProductService(ApplicationDbContext dbContext
            , IEntityRepository<Product, ProductModel> entityRepository)
            : base(dbContext)
        {
            _entityRepository = entityRepository;
        }

        public async Task<int> AddProductAsync(Product product)
        {
            product = (await _entityRepository.AddAsync(product)).Entity;

            foreach (var rp in product.RelatedProducts)
            {
                rp.Product1 = product;
            }

            return await _entityRepository.SaveChangesAsync();
        }
    }
}
