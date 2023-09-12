using BackEnd.Core.Enums;

namespace BackEnd.Core.Dtos.Company
{
	public class CompanyCreateDto
	{
        public string Name { get; set; }
        public CompanySize  Size { get; set; }
    }
}
