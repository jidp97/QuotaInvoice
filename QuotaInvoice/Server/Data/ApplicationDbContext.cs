using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuotaInvoice.Shared.Entities;

namespace QuotaInvoice.Server.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<MetroCard> MetroCards { get; set; }
        public DbSet<TravelHistory> TravelsHistory { get; set; }
    }
}
