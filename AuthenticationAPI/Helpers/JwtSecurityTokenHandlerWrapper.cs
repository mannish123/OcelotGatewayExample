using AuthenticationAPI.Entity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationAPI.Helpers
{
    public class JwtSecurityTokenHandlerWrapper
    {
        private readonly List<User> _users = new()
        {
            new ("manish","Admin", "123"),
            new("shiva","Dev", "321")
        };
        IConfiguration configuration;
        public JwtSecurityTokenHandlerWrapper(IConfiguration configuration)
        {
            
            this.configuration = configuration;
        }

        // Generate a JWT token based on user ID and role
        public string GenerateJwtToken(User userData)
        {
            try
            {
                var user = _users.FirstOrDefault(u => u.UserName == userData.UserName
                                          && u.Password == userData.Password);
                if (user is null)
                {
                    return null;
                }
                var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);

                var secretKey = new SymmetricSecurityKey(key);
                var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var expirationTimeStamp = DateTime.Now.AddMinutes(5);
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                    new Claim("password", string.Join(" ", user.Password)),
                    new Claim("role", string.Join(" ", user.Role))
                };
                var tokenOptions = new JwtSecurityToken(
                    issuer: "https://localhost:5002",
                    claims: claims,
                    expires: expirationTimeStamp,
                    signingCredentials: signingCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return tokenString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
