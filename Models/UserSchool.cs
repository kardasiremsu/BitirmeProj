using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BitirmeProj.Models
{
    public class UserSchool
    {
        [Key]
        public int UserSchoolID { get; set; }
        [ForeignKey("UserID")]

        public int UserID { get; set; } 
        //[ForeignKey("SchoolID")]
        public int ?SchoolID { get; set; }
        public int GraduationYear { get; set; }
        public string Name { get; set; }
        public string? Degree { get; set; }
        //public User User { get; set; }

       
        //public School School { get; set; }
    }
}
