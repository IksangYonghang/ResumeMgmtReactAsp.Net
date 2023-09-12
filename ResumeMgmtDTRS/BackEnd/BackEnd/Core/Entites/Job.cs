using BackEnd.Core.Enums;
using System.Reflection.Metadata.Ecma335;

namespace BackEnd.Core.Entites
{
	public class Job : BaseEntity
	{
        public string Title { get; set; }
        public JobLevel Level { get; set; }

        //Relation with company
        public long CompanyId { get; set; }
        public Company Company { get; set; }
        public ICollection<Candidate> Candidates { get; set; }
    }
}
