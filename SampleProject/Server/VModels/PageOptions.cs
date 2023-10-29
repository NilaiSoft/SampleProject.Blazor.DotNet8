namespace SampleProject.Server.VModels
{
    public class PageOptions<TModel>
    {
        public IList<TModel> Models { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
