using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BitirmeProj.Models
{
    public class Application
    {
        [Key]
        public int ApplicationID { get; set; }

        public int UserID { get; set; }
        public int JobID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string Status { get; set; }
        public string ApplicationNote { get; set; }
        public string CoverLetter { get; set; }
        public int CVID { get; set; }

        // Foreign keys
        [ForeignKey("UserID")]
        public User User { get; set; }

        [ForeignKey("JobID")]
        public JobListing JobListing { get; set; }

        [ForeignKey("CVID")]
        public CV CV { get; set; }
    }
}
