using AuthenticationAPI.Entity;
using AuthenticationAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[EnableRateLimiting("fixed")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        IConfiguration configuration;
        // Create an instance of JwtSecurityTokenHandler
        private readonly JwtSecurityTokenHandlerWrapper _jwtSecurityTokenHandler;
        public AuthController(IConfiguration configuration, JwtSecurityTokenHandlerWrapper jwtSecurityTokenHandler, ILogger<AuthController> logger)
        {
            this.configuration = configuration;
            _jwtSecurityTokenHandler = jwtSecurityTokenHandler;
            _logger = logger;
        }
        [HttpPost]
        public IActionResult Login([FromBody] User user)
        {
            _logger.LogInformation("Generating Jwt Security Token...........");
            var loginResult = _jwtSecurityTokenHandler.GenerateJwtToken(user);
            _logger.LogInformation("Failed to Generate Jwt Security Token.");
            return loginResult is null ? Unauthorized() : Ok(loginResult);
        }
    }
}
