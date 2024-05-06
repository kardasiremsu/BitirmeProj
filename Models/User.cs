using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BitirmeProj.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Key] public int UserID { get; set; }
        public string UserName { get; set; }
        public int UserType { get; set; } // 0 : Student 1: Researcher 
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Gender { get; set; } //0: Female, 1:Male
        public string Title { get; set; }
        public string Phone { get; set; }
      
        public string Address { get; set; }
        public string? Institution { get; set; }
        public int? Token {  get; set; }
        public bool isActive { get; set; }

        public string? ProfilePhotoURL { get; set; } // Add this property for the profile photo URL

        public ICollection<CV> CVs { get; set; }
        public ICollection<UserSchool> UserSchools { get; set; }
        public ICollection<UserWork> UserWorks { get; set; }
        public ICollection<UserReference> UserReferences { get; set; }
        public ICollection<UserLanguage> UserLanguages { get; set; }
        public ICollection<UserSkill> UserSkills { get; set; }
        public User()
        {
            CVs = new List<CV>();
             // Navigation properties for relationships
    
       
            UserSchools = new List<UserSchool>();
            UserWorks = new List<UserWork>();
            UserReferences = new List<UserReference>();
            UserLanguages = new List<UserLanguage>();
            UserSkills = new List<UserSkill>();
        
    }

    }
}
