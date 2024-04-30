namespace BitirmeProj.Models
{
    public class UserProfileViewModel
    {
        public User User { get; set; } // Assuming you have a User class defined
        public List<int>? JobApplicationIDs { get; set; }
        public List<CV>? CV { get; set; }
        public List<Skill>? Skill { get; set; }
        public List<School>? School { get; set; }
        public List<UserSkill>? UserSkill { get; set; }

    }
}
