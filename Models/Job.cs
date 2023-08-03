using System.ComponentModel.DataAnnotations;

namespace BitirmeProj.Models
{
	public class Job
	{
		[Key]

        public int ID { get; set; }
		public int PersonID { get; set; }
		public string Title { get; set; }

        public string? JobLocation { get; set; }
        public int? JobType { get; set; } 
		/*
		0 - Full time
		1 - Part time
		2 - Contract
		3 - Temporary
		4 - Other
		5 - Volunteer
		6 - Internship
		 */

		public string? Description { get; set; }

        public string? Salary { get; set; }

        public string? Skills { get; set; }

		public string? Questions { get; set; }

		public int Active { get; set; } //0 - Inactive 1 - Active

		public Person? Applications { get; set; }

		public string Date { get; set; }

    }
}
