using Microsoft.AspNetCore.Identity;

namespace QuotaInvoice.Server.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Image { get; set; }
        public string? Cargo { get; set; }
    }
}
