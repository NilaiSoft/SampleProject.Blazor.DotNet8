using SampleProject.Server.Data;
using SampleProject.Server.VModels;

namespace SampleProjects.Server.Services
{
    public class RelatedProductService
        : EntityRepository<RelatedProduct, RelatedProductModel>, IRelatedProductService
    {
        public RelatedProductService(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
