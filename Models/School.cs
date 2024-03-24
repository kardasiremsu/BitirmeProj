using System.ComponentModel.DataAnnotations;

namespace BitirmeProj.Models
{
    public class School
    {
        [Key]
        public int SchoolID { get; set; }
        public string Name { get; set; }
        public string? Degree {  get; set; }
        // Navigation property
        public ICollection<CVSchool> CVSchools { get; set; }
    }
}
