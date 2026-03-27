
using OcelotGateway.Helpers;
using System.Security.Claims;

namespace OcelotGateway.Middlewares
{
    public class JwtMiddleware : IMiddleware
    {
        private readonly JwtSecurityTokenHandlerWrapper _jwtSecurityTokenHandler;

        public JwtMiddleware(JwtSecurityTokenHandlerWrapper jwtSecurityTokenHandler)
        {
            _jwtSecurityTokenHandler = jwtSecurityTokenHandler;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (!token.IsWhiteSpace())
            {

                var claimPrinciple = _jwtSecurityTokenHandler.ValidateJwtToken(token);
                // Extract the user ID from the token
                var userName = claimPrinciple.FindFirst(ClaimTypes.NameIdentifier)?.Value;


                // Check if the user ID exists in the dictionary.
                if (!userName.Equals("manish"))
                {
                    // If the token is invalid, throw an exception
                    throw new UnauthorizedAccessException();
                }
                else
                {
                    context.Items.Add("UserName", userName);
                }
            }
            await next(context);
        }
    }
}
