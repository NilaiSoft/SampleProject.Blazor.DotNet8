using System.ComponentModel.DataAnnotations;

namespace SampleProject.Server.Data
{
    public abstract partial class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
