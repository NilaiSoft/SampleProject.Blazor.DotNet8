namespace SampleProject.Server.Data
{
    public interface ISoftDeletedEntity
    {
        bool Deleted { get; set; }
    }
}
