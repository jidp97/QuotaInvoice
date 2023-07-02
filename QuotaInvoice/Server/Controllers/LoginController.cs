using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QuotaInvoice.Server.Data;
using QuotaInvoice.Shared.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace QuotaInvoice.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginController(IConfiguration configuration,
                               SignInManager<ApplicationUser> signInManager)
        {
            _configuration = configuration;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            var result = await _signInManager.PasswordSignInAsync(userName: login.Email ?? throw new NullReferenceException(message: "Username not found."),
                                                                  password: login.Password ?? throw new NullReferenceException(message: "Enter a password"),
                                                                  isPersistent: false,
                                                                  lockoutOnFailure: false);

            if (!result.Succeeded) return BadRequest(error: new LoginResult { Successful = false, Error = "Username and password are invalid." });

            var user = await _signInManager.UserManager.FindByEmailAsync(login.Email);
            var roles = await _signInManager.UserManager.GetRolesAsync(user: user ?? throw new NullReferenceException(message: "User not found."));
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, login.Email)
            };

            foreach (var role in roles)
            {
                claims.Add(item: new Claim(type: ClaimTypes.Role,value: role));
            }

            var key = new SymmetricSecurityKey(key: Encoding.UTF8.GetBytes(s: _configuration["JwtSecurityKey"] ?? throw new NullReferenceException(message: "Empty JwtSecurityKey")));
            var creds = new SigningCredentials(key: key,algorithm: SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(value: Convert.ToInt32(_configuration["JwtExpiryInDays"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtIssuer"],
                audience: _configuration["JwtAudience"],
                claims: claims,
                expires: expiry,
                signingCredentials: creds
            );

            return Ok(value: new LoginResult { Successful = true, Token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}