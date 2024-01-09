using System.ComponentModel.DataAnnotations;

namespace BitirmeProj.Models
{
    public class User
    {
        [Key] public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserType { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Gender { get; set; }
        public string Title { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
       
        public ICollection<CV> CVs { get; set; }
    }
}
