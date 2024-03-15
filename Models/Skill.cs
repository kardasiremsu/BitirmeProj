using System.ComponentModel.DataAnnotations;

namespace BitirmeProj.Models
{
    public class Skill
    {
        [Key]
        public int SkillID { get; set; }
        public int Name { get; set; }

        // Navigation property
        public ICollection<CVSkill> CVSkills { get; set; }
    }
}
