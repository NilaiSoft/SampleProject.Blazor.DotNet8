using SampleProjects.Shared.Dtos;
using SampleProjects.Shared.ViewModels.Product;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleProject.Shared.Dtos
{
    public class RelatedProductDto
    {
        public int ProductId1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey(nameof(Product2))]
        public int ProductId2 { get; set; }

        /// <summary>
        /// 
        /// </summary>   
        public ProductDto Product2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int DisplayOrder { get; set; }
    }
}
