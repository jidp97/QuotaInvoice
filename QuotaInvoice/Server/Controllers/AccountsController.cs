using QuotaInvoice.Shared;
using QuotaInvoice.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using QuotaInvoice.Server.Data;

namespace QuotaInvoice.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        //private static readonly UserModel LoggedOutUser = new() { IsAuthenticated = false };
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountsController(UserManager<ApplicationUser> userManager) => _userManager = userManager;

      //  [HttpPost, Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] RegisterModel model)
        {
            ApplicationUser newUser = new() { UserName = model.Email, Email = model.Email };

            var result = await _userManager.CreateAsync(newUser, model.Password);
         
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);

                return BadRequest(new RegisterResult { Successful = false, Errors = errors });
            }
            // Add all new users to the User role
            await _userManager.AddToRoleAsync(newUser, "User");

             //Add new users whose email starts with 'admin' to the Admin role
            if (newUser.Email.StartsWith("admin"))
            {
                await _userManager.AddToRoleAsync(newUser, "Admin");
            }

            return Ok(new RegisterResult { Successful = true });
        }
    }
}