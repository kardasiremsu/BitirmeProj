namespace BitirmeProj.Models
{
    public class UserProfileViewModel
    {
        public User User { get; set; } // Assuming you have a User class defined
        public List<int>? JobApplicationIds { get; set; }
        public List<CV>? CV { get; set; }
    }
}
