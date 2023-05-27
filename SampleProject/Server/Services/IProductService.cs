using SampleProject.Server.Data;
using SampleProject.Server.VModels;

namespace SampleProjects.Server.Services
{
    public interface IProductService : IEntityRepository<Product, ProductModel>
    {
    }
}
