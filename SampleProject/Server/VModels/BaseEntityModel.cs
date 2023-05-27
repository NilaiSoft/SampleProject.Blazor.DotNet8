namespace SampleProject.Server.VModels
{
    public partial record BaseEntityModel : BaseModel
    {
        /// <summary>
        /// Gets or sets model identifier
        /// </summary>
        public virtual int Id { get; set; }
    }
}
