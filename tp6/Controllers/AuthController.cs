using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using tp6.Models;

namespace tp6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JWTBearerTokenSettings _jwtSettings;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(IOptions<JWTBearerTokenSettings> jwtOptions, UserManager<IdentityUser> userManager)
        {
            _jwtSettings = jwtOptions.Value;
            _userManager = userManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterCredentials details)
        {
            var user = new IdentityUser { UserName = details.Username, Email = details.Email };
            var result = await _userManager.CreateAsync(user, details.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);
            return Ok(new { Message = "Registration successful" });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginCredentials credentials)
        {
            var user = await _userManager.FindByNameAsync(credentials.Username);
            if (user == null || await _userManager.CheckPasswordAsync(user, credentials.Password) == false)
                return Unauthorized();

            var token = GenerateToken(user);
            return Ok(new { Token = token });
        }

        private string GenerateToken(IdentityUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.UserName), new Claim(ClaimTypes.Email, user.Email) }),
                Expires = DateTime.UtcNow.AddSeconds(_jwtSettings.ExpireTimeInSeconds),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience
            };
            var token = tokenHandler.CreateToken(descriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
