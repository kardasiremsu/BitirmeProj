using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BitirmeProj.Models
{
    public class CVSchool
    {
        [Key]
        public int CVSchoolID { get; set; }

        public int CVID { get; set; }
        public int SchoolID { get; set; }
        public int GraduationYear { get; set; }

        // Foreign keys
        [ForeignKey("CVID")]
        public CV CV { get; set; }

        [ForeignKey("SchoolID")]
        public School School { get; set; }
    }
}
