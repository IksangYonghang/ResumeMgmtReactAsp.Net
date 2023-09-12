using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;

namespace BackEnd.Core.Entites
{
	public abstract class BaseEntity
	{
        [Key]
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
    }
}
