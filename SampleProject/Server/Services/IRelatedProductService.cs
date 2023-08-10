using SampleProject.Server.Data;
using SampleProject.Server.VModels;

namespace SampleProjects.Server.Services
{
    public interface IRelatedProductService : IEntityRepository<RelatedProduct, RelatedProductModel>
    {
    }
}
