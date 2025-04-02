using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace hello2.Controllers // ✅ Corrected namespace
{
    [Route("api/hello")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpGet("VerifyUser")]
        [Authorize] // Ensures JWT token is required for this API
        public IActionResult VerifyUser()
        {
            // Extract username from the JWT token
            var identity = HttpContext.User.Identity as System.Security.Claims.ClaimsIdentity;

            if (identity != null && identity.Claims.Any())
            {
                var username = identity.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;

                return Ok(new
                {
                    Username = username,
                    Message = "JWT Token is valid!"
                });
            }

            return Unauthorized(new { Message = "Invalid token or no username found!" });
        }
    }
}
