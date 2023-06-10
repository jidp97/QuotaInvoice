using System.Collections.Generic;

namespace QuotaInvoice.Shared.Models
{
    public class RegisterResult
    {
        public bool Successful { get; set; }
        public required IEnumerable<string> Errors { get; set; }
    }
}