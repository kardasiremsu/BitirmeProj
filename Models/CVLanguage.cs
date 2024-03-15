using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BitirmeProj.Models
{
    public class CVLanguage
    {
        [Key]
        public int CVLanguagesID { get; set; }

        public int CVID { get; set; }
        public int LanguageID { get; set; }
        public int Level { get; set; }

        // Foreign keys
        [ForeignKey("CVID")]
        public CV CV { get; set; }

        [ForeignKey("LanguageID")]
        public Language Language { get; set; }
    }
}
