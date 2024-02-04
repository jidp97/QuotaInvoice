﻿using QuotaInvoice.Shared.Models;
using System.Threading.Tasks;

namespace QuotaInvoice.Client.Services
{
    public interface IAuthService
    {
        Task<LoginResult> Login(LoginModel loginModel);

        Task Logout();

        Task<RegisterResult> Register(RegisterModel registerModel);
    }
}