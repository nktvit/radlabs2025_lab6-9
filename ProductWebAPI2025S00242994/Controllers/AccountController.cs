using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tracker.WebAPIClient;

namespace ProductWebAPI2025.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("Token")]
        public async Task<IActionResult> Token([FromBody] LoginViewModel model)
        {
            ActivityAPIClient.Track(StudentID: "S00242994", StudentName: "Mykyta Vitkovsky", 
                activityName: "RAD30223 Week 8 Lab 1", Task: "Implementing Blazor GRN components");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(model.Username);
            if (user == null)
            {
                return Unauthorized();
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized();
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"] ?? "YourSecretKeyHere12345678901234567890"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(1);

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"] ?? "YourIssuer",
                _configuration["JwtAudience"] ?? "YourAudience",
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return Ok(new
            {
                access_token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = expires
            });
        }
    }

    public class LoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
