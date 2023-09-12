using BackEnd.Core.Enums;
using System.Reflection.Metadata.Ecma335;

namespace BackEnd.Core.Entites
{
	public class Company : BaseEntity
	{
        public string Name { get; set; }
		public CompanySize Size { get; set; }

		//Relations with the Job
        public ICollection<Job> Jobs { get; set; }

    }
}
