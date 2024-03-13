using System.ComponentModel.DataAnnotations;

namespace BitirmeProj.Models
{
	public class Schools
	{
		[Key]
		public int SchoolID { get; set; }
		public string SchoolName { get; set; }
		public string SchoolDepartment { get; set; }
		public string SchoolStartDate { get; set; }
		public string SchoolEndDate { get; set; }
		public string SchoolGPA { get; set; }
	}
}
