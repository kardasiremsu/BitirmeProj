using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BitirmeProj.Models
{
    public class CVSkill
    {
        [Key]
        public int CVSkillID { get; set; }

        public int CVID { get; set; }
        public int SkillID { get; set; }
        public int Experience { get; set; }

        // Foreign keys
        [ForeignKey("CVID")]
        public CV CV { get; set; }

        [ForeignKey("SkillID")]
        public Skill Skill { get; set; }
    }
    
}
