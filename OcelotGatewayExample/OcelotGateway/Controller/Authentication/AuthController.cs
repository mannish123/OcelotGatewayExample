using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using OcelotGateway.Entity;
using OcelotGateway.Helpers;

namespace OcelotGateway.Controller.Authentication
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
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
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Auth([FromBody] User user)
        {
            _logger.LogInformation("Generating Jwt Security Token...........");
            IActionResult response = new UnauthorizedResult();

            if (user != null && user.UserName.Equals("manish") && user.Password.Equals("123"))
            {
                var jwtToken = _jwtSecurityTokenHandler.GenerateJwtToken(user.UserName, user.Password);

                _logger.LogInformation("Generated Jwt Security Token.");
                return Ok(jwtToken);
            }
            _logger.LogInformation("Failed to Generate Jwt Security Token.");
            return response;
        }

    }
}
