using QuotaInvoice.Server.Data;
using QuotaInvoice.Server.Helpers;
using QuotaInvoice.Shared.DTOs;
using QuotaInvoice.Shared.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace QuotaInvoice.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public UsuariosController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<UsuarioDTO>>> Get([FromQuery] PaginationModel paginacion)
        {
            var queryable = context.Users.AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnRespuesta(queryable, paginacion.CantidadAMostrar);
            return await queryable.Paginar(paginacion)
                .Select(x => new UsuarioDTO { Email = x.Email, UserId = x.Id, FirtName = x.FirstName, LastName = x.LastName }).ToListAsync();
        }

        [HttpGet("roles")]
        public async Task<ActionResult<List<RolDTO>>> Get() => await context.Roles
                .Select(x => new RolDTO { Nombre = x.Name, RoleId = x.Id }).ToListAsync();

        [HttpGet("{UserId}/obtenerUsuario")]
        public async Task<ActionResult<UsuarioDTO>> GetAsync(string UserId) => await context.Users
                .Select(x => new UsuarioDTO
                {
                    Email = x.Email,
                    UserId = x.Id,
                    FirtName = x.FirstName,
                    LastName = x.LastName,
                    Cargo = x.Cargo
                })
                .FirstOrDefaultAsync(x => x.UserId == UserId);

        [HttpPut]
        public async Task<ActionResult> PutAsync(UsuarioDTO usuario)
        {
            var user = await userManager.FindByIdAsync(usuario.UserId);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userManager.FindByIdAsync(usuario.UserId)}'.");
            }

            var email = user.Email;
            if (usuario.Email != email)
            {
                var setEmailResult = await userManager.SetEmailAsync(user, usuario.Email);
                if (!setEmailResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting email for user with ID '{user.Id}'.");
                }
            }

            if (usuario.FirtName != user.FirstName)
            {
                user.FirstName = usuario.FirtName;
            }
            if (usuario.LastName != user.LastName)
            {
                user.LastName = usuario.LastName;
            }

            await userManager.UpdateAsync(user);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarUsuario(string id)
        {
            var usuario = new UsuarioDTO { UserId = id };
            var user = await userManager.FindByIdAsync(usuario.UserId);
            await userManager.DeleteAsync(user);
            return NoContent();
        }

        [HttpPost("asignarRol")]
        public async Task<ActionResult> AsignarRolUsuario(EditarRolDTO editarRolDTO)
        {
            var usuario = await userManager.FindByIdAsync(userId: editarRolDTO.UserId);
            await userManager.AddToRoleAsync(user: usuario, role: editarRolDTO.RoleId);
            return NoContent();
        }

        [HttpPost("removerRol")]
        public async Task<ActionResult> RemoverUsuarioRol(EditarRolDTO editarRolDTO)
        {
            var usuario = await userManager.FindByIdAsync(userId: editarRolDTO.UserId);
            await userManager.RemoveFromRoleAsync(user: usuario, role: editarRolDTO.RoleId);
            return NoContent();
        }
    }
}