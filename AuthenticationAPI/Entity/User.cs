namespace AuthenticationAPI.Entity
{
    public class User
    {
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public string? Address { get; set; }

        public User(string userName, string role, string password)
        {
            UserName= userName;
            Role= role;
            Password=password;
        }
    }
}
