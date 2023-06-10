namespace QuotaInvoice.Shared.Models
{
    public class UserModel
    {
        public required string Email { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}