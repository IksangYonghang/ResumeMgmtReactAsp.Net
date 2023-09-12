using BackEnd.Core.Enums;

namespace BackEnd.Core.Dtos.Company
{
	public class CompanyUpdateDto
	{
		public string Name { get; set; }
		public CompanySize Size { get; set; }
	}
}
