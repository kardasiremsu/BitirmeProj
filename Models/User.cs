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
       
        public ICollection<CV> CVs { get; set; }
    }
}
