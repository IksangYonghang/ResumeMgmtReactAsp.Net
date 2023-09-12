using BackEnd.Core.Enums;

namespace BackEnd.Core.Dtos.Job
{
	public class JobGetDto
	{
		public long Id { get; set; }
		public bool IsActive { get; set; } = true;
		public string Title { get; set; }
		public JobLevel Level { get; set; }		
		public long CompanyId { get; set; }
        public string CompanyName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;	
		
	}
}
