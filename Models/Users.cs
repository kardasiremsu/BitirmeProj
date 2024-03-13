using System.ComponentModel.DataAnnotations;

namespace BitirmeProj.Models
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }
        [DataType(DataType.Password)]
        public string Username { get; set; }
		public string UserType { get; set; }
		public string Email { get; set; }
		[DataType(DataType.Password)]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
		public string Gender { get; set; }
        public string Title { get; set; }
		public string? Phone { get; set; }
        public string? Address { get; set; }

        public List<CV> CV { get; set; }


    }
}
