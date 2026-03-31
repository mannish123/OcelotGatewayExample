using AuthenticationAPI.Entity;

namespace AuthenticationAPI.Services
{
    public interface IAuthService
    {
        public IEnumerable<User> GetUserList();
        public User GetUserById(int id);
        public User AddUser(User product);
        public User UpdateUser(User product);
        public bool DeleteUser(int Id);
    }
}
