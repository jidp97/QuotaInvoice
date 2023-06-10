﻿using System.ComponentModel.DataAnnotations;

namespace QuotaInvoice.Shared.Models
{
    public class LoginModel
    {
        [Required]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}