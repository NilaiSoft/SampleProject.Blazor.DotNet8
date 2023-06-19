using SampleProjects.Shared.Dtos;
using SampleProjects.Shared.ViewModels.Product;

namespace SampleProject.Shared.Dtos
{
    public class RelatedProductDto
    {
        public int ProductId1 { get; set; }

        public string ProductName1 { get; set; }

        public int ProductId2 { get; set; }

        public string ProductName2 { get; set; }

        public int DisplayOrder { get; set; }

        public ProductDto ProductDto { get; set; }
    }
}
