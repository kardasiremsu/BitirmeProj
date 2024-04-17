using System.ComponentModel.DataAnnotations;

namespace BitirmeProj.Models
{
    public class Skill
    {
        [Key]
        public int SkillID { get; set; }
        public string Name { get; set; }

        // Navigation property
        public ICollection<UserSkill>? UserSkills { get; set; }
    }
}
