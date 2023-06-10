﻿using QuotaInvoice.Shared.Models;

namespace QuotaInvoice.Client.Services
{
    public interface IAuthService
    {
        Task<LoginResult> Login(LoginModel loginModel);

        Task Logout();

        Task<RegisterResult> Register(RegisterModel registerModel);
    }
}