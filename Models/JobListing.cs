using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BitirmeProj.Utilities;


namespace BitirmeProj.Models
{
    public class JobListing
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JobID { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public DateTime ApplicationDeadline { get; set; }
        public int PostedBy { get; set; }
        public string JobLocation { get; set; }
        public int WorkPlaceType { get; set; }
        public int JobType { get; set; }
        public int ExperienceLevel { get; set; }
        public int Salary { get; set; }
        public int SalaryCurrency { get; set; }
        public int IsActive { get; set; }
        public DateTime JobCreatedDate { get; set; }

        // Foreign key
        [ForeignKey("PostedBy")]
        public User User { get; set; }

        // Navigation property
        public ICollection<JobListingSkill> JobListingSkills { get; set; }
        public ICollection<Application> Applications { get; set; }
    }
    
}
