using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BitirmeProj.Models
{
    public class CV
    {
        [Key]
        public int CVID { get; set; }

        public int UserID { get; set; }
        public byte[]? CVFile { get; set; } // Replace with appropriate data type
        public int LastUpdateDate { get; set; }
        public int CreationDate { get; set; }
        public string CVName { get; set; }

        // Foreign key
        [ForeignKey("UserID")]
        public User User { get; set; }

        // Navigation properties for relationships
        public ICollection<CVSchool> CVSchools { get; set; }
        public ICollection<CVWork> CVWorks { get; set; }
        public ICollection<CVReference> CVReferences { get; set; }
        public ICollection<CVLanguage> CVLanguages { get; set; }
        public ICollection<CVSkill> CVSkills { get; set; }
        public CV()
        {
            CVSchools = new List<CVSchool>();
            CVWorks = new List<CVWork>();
            CVReferences = new List<CVReference>();
            CVLanguages = new List<CVLanguage>();
            CVSkills = new List<CVSkill>();
        }
    }
}
