using System.Linq;
using BitirmeProj.Data; // Assuming your DbContext is named ApplicationDBContext
using BitirmeProj.Models; // Assuming your User model is in this namespace

namespace BitirmeProj.Utilities
{
    public static class UserService
    {
        public static string GetUserFullName(int userId, ApplicationDBContext context)
        {
            var user = context.Users.FirstOrDefault(u => u.UserID == userId);

            return user != null ? $"{user.FirstName} {user.LastName}" : "Unknown User";
        }
        public static string GetUserInstitution(int userId, ApplicationDBContext context)
        {
            var user = context.Users.FirstOrDefault(u => u.UserID == userId);
            return user.Institution;
        }
    }
}
