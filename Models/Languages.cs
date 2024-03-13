using System.ComponentModel.DataAnnotations;

namespace BitirmeProj.Models
{
	public class Languages
	{
		[Key]
		public int LanguageID { get; set; }
		public string LanguageName { get; set; }
		public string LanguageLevel { get; set; }
	}
}
