using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Authentication.Models;
using Authentication.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace Authentication.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly APIContext _context;

        public AuthController(APIContext context)
        {
            _context = context;
        }
        private string GenerateJwtToken(Apage user)
        {
            var jwtSettings = _context.Database.GetDbConnection().ConnectionString;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsASecretKeyForJwtTokenGeneration"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("username", user.Username),
                new Claim("name", user.Name)
            };

            var token = new JwtSecurityToken(
                issuer: "YourApp",
                audience: "YourUsers",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        // Login
        [HttpGet]
        public IActionResult Login([FromBody] Apage check)
        {
            var checkInDB = _context.ApageTable
                .FirstOrDefault(u => u.Username == check.Username);

            if (checkInDB == null || checkInDB.Password != check.Password)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }

            var token = GenerateJwtToken(checkInDB);
            return Ok(new { token, username = checkInDB.Username, name = checkInDB.Name });
        }


        // SignIn (Register a new user)
        [HttpPost]
        public IActionResult SignIn([FromBody] Apage add)
        {
            var existingUser = _context.ApageTable
                .FirstOrDefault(u => u.Username == add.Username);

            if (existingUser != null)
            {
                return Conflict(new { message = "Username already exists." });
            }
            add.Id = 0;

            // Insert new user into the database
            _context.ApageTable.Add(add);
            _context.SaveChanges();

            var token = GenerateJwtToken(add);
            return Ok(new { message = $"Welcome {add.Username}", token });
        }


        [HttpPost]
        public IActionResult Hello_World([FromBody] TokenRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Token))
            {
                return Unauthorized(new { message = "Token is required" });
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("ThisIsASecretKeyForJwtTokenGeneration");

            try
            {
                tokenHandler.ValidateToken(request.Token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return Ok(new { message = "Hello World! Token is valid." });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = "Invalid or expired token.", error = ex.Message });
            }
        }
        [HttpGet]
        [Authorize]  // This will automatically validate the token
        public IActionResult VerifyUser()
        {
            // Extract claims from JWT
            var username = User.FindFirst("username")?.Value;
            var name = User.FindFirst("name")?.Value;

            return Ok(new { message = "User is verified", username, name });
        }

        public class TokenRequest
        {
            public string? Token { get; set; }
        }

    }
}
