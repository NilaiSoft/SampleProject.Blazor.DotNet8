namespace SampleProject.Server.Data
{
  public partial class Product : BaseEntity, ISoftDeletedEntity
  {
    public Product()
    {
      Name = string.Empty;
      ShortDescription = string.Empty;
      FullDescription = string.Empty;
      AdminComment = string.Empty;
      ShowOnHomepage = true;
      Enable = true;
      Deleted = false;
    }

    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the short description
    /// </summary>
    public string ShortDescription { get; set; }

    /// <summary>
    /// Gets or sets the full description
    /// </summary>
    public string FullDescription { get; set; }

    /// <summary>
    /// Gets or sets the admin comment
    /// </summary>
    public string AdminComment { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool ShowOnHomepage { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool Enable { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public bool Deleted { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool Published { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public virtual ICollection<RelatedProduct> RelatedProducts { get; set; }
  }
}
