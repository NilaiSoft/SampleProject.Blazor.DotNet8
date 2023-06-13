using System.ComponentModel.DataAnnotations.Schema;

namespace SampleProject.Server.Data
{
    public class RelatedProduct : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [ForeignKey(nameof(Product1))]
        public int? ProductId1 { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [ForeignKey(nameof(Product2))]
        public int? ProductId2 { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public Product Product1 { get; set; }

        /// <summary>
        /// 
        /// </summary>   
        [NotMapped]
        public Product Product2 { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int DisplayOrder { get; set; }
    }
}
