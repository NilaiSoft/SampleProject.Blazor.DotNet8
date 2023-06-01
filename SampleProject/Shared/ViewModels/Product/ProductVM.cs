namespace SampleProjects.Shared.ViewModels.Product
{
    public class ProductVM
    {
        public ProductVM()
        {
            Name = string.Empty;
            ShortDescription = string.Empty;
            FullDescription = string.Empty;
            AdminComment = string.Empty;
            ShowOnHomepage = true;
            Deleted = false;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public string AdminComment { get; set; }
        public bool ShowOnHomepage { get; set; }
        public bool Enable { get; set; }
        public bool Deleted { get; set; }
    }
}
