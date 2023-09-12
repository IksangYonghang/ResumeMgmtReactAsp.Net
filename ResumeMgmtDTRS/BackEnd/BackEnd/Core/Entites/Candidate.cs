using System.Reflection.Metadata.Ecma335;

namespace BackEnd.Core.Entites
{
	public class Candidate : BaseEntity	
	{
        public string FirstName { get; set; }
		public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CoverLetter { get; set; }
        public string ResumeUrl { get; set; }

        //Relations with job
        public long JobId { get; set; }
        public Job Job { get; set; }

    }
}
