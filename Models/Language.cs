using System.ComponentModel.DataAnnotations;

namespace BitirmeProj.Models
{
    public class Language
    {
        [Key]
        public int LanguageID { get; set; }
        public string Name { get; set; }
        //public string? Proficiency {  get; set; } 
        // Navigation property
       // public ICollection<UserLanguage>? UserLanguages { get; set; }
    }
}
