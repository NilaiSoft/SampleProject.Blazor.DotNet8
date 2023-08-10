using System.ComponentModel.DataAnnotations.Schema;

namespace SampleProject.Server.Data
{
    public class RelatedProduct : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int ProductId1 { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [ForeignKey(nameof(Product2))]
        public int ProductId2 { get; set; }

        /// <summary>
        /// 
        /// </summary>   
        public Product Product2 { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int DisplayOrder { get; set; }
    }
}
