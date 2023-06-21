namespace SampleProject.Server.VModels
{
    public class RelatedProductModel : BaseModel
    {
        public int ProductId1 { get; set; }
        
        public int ProductId2 { get; set; }

        public int DisplayOrder { get; set; }
    }
}
