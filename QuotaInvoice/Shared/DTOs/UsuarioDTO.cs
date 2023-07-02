using System.ComponentModel.DataAnnotations;

namespace QuotaInvoice.Shared.DTOs
{
    public class UsuarioDTO
    {
        [Required]
        public string? UserId { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? FirtName { get; set; }

        [Required]
        public string? LastName { get; set; }
    }
}