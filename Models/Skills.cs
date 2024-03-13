using System.ComponentModel.DataAnnotations;

namespace BitirmeProj.Models
{
	public class Skills
	{
		[Key]
		public int SkillID { get; set; }
		public string SkillName { get; set; }
		public string SkillLevel { get; set; }
	}
}
