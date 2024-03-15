using BitirmeProj.Models;

namespace BitirmeProj.Services
{

    public interface IUserSessionService
    {
        void SetCurrentUser(User user);
        User GetCurrentUser();
    }

    public class UserSessionService : IUserSessionService
    {
        private User _currentUser;

        public void SetCurrentUser(User user)
        {
            _currentUser = user;
        }

        public User GetCurrentUser()
        {
            return _currentUser;
        }
    }
}
