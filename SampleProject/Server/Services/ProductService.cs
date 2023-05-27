using SampleProject.Server.Data;
using SampleProject.Server.VModels;

namespace SampleProjects.Server.Services
{
    public class ProductService : EntityRepository<Product, ProductModel>, IProductService
    {
        public ProductService(ApplicationDbContext dbContext) 
            : base(dbContext)
        {
        }
    }
}
