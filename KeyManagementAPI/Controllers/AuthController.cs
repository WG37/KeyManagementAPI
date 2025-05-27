using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KeyManagementAPI.Controllers
{
    
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _config;

        public AuthController(SignInManager<IdentityUser> sM, UserManager<IdentityUser> uM, IConfiguration cfg)
        {
            _signInManager = sM;
            _userManager = uM;
            _config = cfg;
        }

        public record LoginRequest(string Email, string Password);

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null) return Unauthorized();

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);
            if (!result.Succeeded) return Unauthorized();

            var token = GenerateJwt(user);
            return Ok(new { token });
        }

        private string GenerateJwt(IdentityUser user)
        {
            var jwtCfg = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtCfg["Key"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha384);

            // claims
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            return new JwtSecurityTokenHandler().WriteToken(
                new JwtSecurityToken(
                    issuer: jwtCfg["Issuer"],
                    audience: jwtCfg["Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(8),
                    signingCredentials: credentials
                    ));
        }
    }
}

