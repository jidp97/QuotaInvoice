namespace QuotaInvoice.Shared.Models
{
    public class LoginResult
    {
        public bool Successful { get; set; }
        public required string Error { get; set; }
        public required string Token { get; set; }
    }
}