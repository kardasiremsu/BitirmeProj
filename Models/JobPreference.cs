using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BitirmeProj.Models
{
    public class JobPreference
    {
        [Key]
        public int JobPreferencesID { get; set; }

        public int UserID { get; set; }
        public string Location { get; set; }
        public int MinimumSalary { get; set; }
        public int JobType { get; set; }
        public int ExperienceLevel { get; set; }
        public string Title { get; set; }

        // Foreign key
        [ForeignKey("UserID")]
        public User User { get; set; }
    }
}
