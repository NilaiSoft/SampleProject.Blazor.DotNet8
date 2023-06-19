using SampleProjects.Shared.Dtos;
using SampleProjects.Shared.ViewModels.Product;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleProject.Shared.Dtos
{
    public class RelatedProductDto
    {
        public int ProductId1 { get; set; }
        public int ProductId2 { get; set; }
        public ProductDto Product2 { get; set; }
        public int DisplayOrder { get; set; }
    }
}
