using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BitirmeProj.Models
{
    public class JobListingSkill
    {
        [Key]
        public int JobListingSkillsID { get; set; }

        public int JobListingsID { get; set; }
        public int SkillID { get; set; }
        public int Experience { get; set; }

        // Foreign keys
        [ForeignKey("JobListingsID")]
        public JobListing JobListing { get; set; }

        [ForeignKey("SkillID")]
        public Skill Skill { get; set; }
    }
}
