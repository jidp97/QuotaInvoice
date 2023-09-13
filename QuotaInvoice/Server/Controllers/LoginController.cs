﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QuotaInvoice.Server.Data;
using QuotaInvoice.Shared.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

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
            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);

            if (!result.Succeeded) return BadRequest(new LoginResult { Successful = false, Error = "Username and password are invalid." });

            ApplicationUser? user = await _signInManager.UserManager.FindByEmailAsync(login.Email);
            IList<string> roles = await _signInManager.UserManager.GetRolesAsync(user);
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, login.Email)
            };
            claims.AddRange(from role in roles
                            select new Claim(ClaimTypes.Role, role));

            string jwtSecretKey = _configuration["JwtSecurityKey"];
            Console.WriteLine(jwtSecretKey);
            byte[] keyBytes = Encoding.UTF8.GetBytes(jwtSecretKey);
            SymmetricSecurityKey key = new(key: keyBytes);
            SigningCredentials creds = new(key: key, algorithm: SecurityAlgorithms.HmacSha256);
            DateTime expiry = DateTime.Now.AddDays(value: Convert.ToInt32(value: _configuration["JwtExpiryInDays"]));

            JwtSecurityToken token = new(
                issuer: _configuration["JwtIssuer"],
                audience: _configuration["JwtAudience"],
                claims: claims,
                expires: expiry,
                signingCredentials: creds
            );
        
            var loginResult = new LoginResult { Successful = true, Token = new JwtSecurityTokenHandler().WriteToken(token) };
            return Ok(value: loginResult);
          
        }
    }
}