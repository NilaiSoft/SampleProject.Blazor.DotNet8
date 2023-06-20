namespace SampleProject.Server.VModels
{
    public class RelatedProductModel : BaseModel
    {
        public int ProductId1 { get; set; }

        public ProductModel? Product2 { get; set; }

        public int DisplayOrder { get; set; }
    }
}
