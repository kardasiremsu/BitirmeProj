using System.ComponentModel.DataAnnotations;

namespace BitirmeProj.Models
{
    public class School
    {
        [Key]
        public int SchoolID { get; set; }
        public int Name { get; set; }

        // Navigation property
        public ICollection<CVSchool> CVSchools { get; set; }
    }
}
