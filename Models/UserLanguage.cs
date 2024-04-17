using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BitirmeProj.Models
{
    public class UserLanguage
    {
        [Key]
        public int UserLanguagesID { get; set; }
 [ForeignKey("UserID")]
        public int UserID { get; set; }

        [ForeignKey("LanguageID")]
        public int LanguageID { get; set; }
        public string ?Proficiency { get; set; }
        public string Name { get; set; }
       
       // public User User { get; set; }


        //public Language Language { get; set; }
    }
}
