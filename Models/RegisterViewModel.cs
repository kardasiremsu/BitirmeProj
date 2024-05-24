using System.ComponentModel.DataAnnotations;
using MailKit;
using Microsoft.AspNetCore.Identity;

namespace BitirmeProj.Models
{
    public class RegisterViewModel : IdentityUser
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required]
        public int UserType { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Gender { get; set; }
        public string Title { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string? Institution { get; set; }
        public bool EmailConfirmed { get; set; }
        public int Token { get; set; }
    }
}
