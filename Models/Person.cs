using System.ComponentModel.DataAnnotations;

namespace BitirmeProj.Models
{
    public class Person
    {
        [Key]
        public int ID { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }

        public CV? CV { get; set; }

        public List<Application>? Applications { get; set; }


    }
}
