using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BitirmeProj.Models
{
    public class UserSkill
    {
        [Key]
        public int UserSkillID { get; set; }
         [ForeignKey("UserID")]
        public int UserID { get; set; }
        [ForeignKey("SkillID")]

        public int SkillID { get; set; }
        public string Experience { get; set; }
        public string Name { get; set; }
      
        //public User User { get; set; }
        
  //      public Skill Skill { get; set; } 
       // [NotMapped] // This property is not mapped to the database
        //public string SkillName => Skill.Name;
    }
    
}
