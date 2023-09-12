using BackEnd.Core.Enums;

namespace BackEnd.Core.Dtos.Job
{
	public class JobUpdateDto
	{
		public string Title { get; set; }
		public JobLevel Level { get; set; }		
		
	}
}
