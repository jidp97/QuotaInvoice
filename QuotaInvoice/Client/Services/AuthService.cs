using Blazored.LocalStorage;
using QuotaInvoice.Shared.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using QuotaInvoice.Client.Helpers;

namespace QuotaInvoice.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;
        private readonly IMostrarMensajes _mostrarMensajes;
        private readonly IHttpResponse _httpResponse;

        public AuthService(HttpClient httpClient,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage,
                           IMostrarMensajes mostrarMensajes,
                           IHttpResponse httpResponse)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
            _mostrarMensajes = mostrarMensajes;
            _httpResponse = httpResponse;
        }

        public async Task<LoginResult> Login(LoginModel loginModel)
        {   
            try
            {
                HttpResponseMessage result = await _httpClient.PostAsJsonAsync("api/Login", loginModel);
                LoginResult? passedresult = await result.Content.ReadFromJsonAsync<LoginResult?>();
                await _localStorage.SetItemAsync(key: "authToken", passedresult.Token);
                ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(passedresult.Token);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", passedresult.Token);
                return passedresult;
            }
            catch (Exception ex)
            {
                if (ex is JsonException)
                {
                    await _mostrarMensajes.MostrarMensajeError(ex.Message.ToString());
                }
                else
                {
                    throw;
                }
            }
            return new LoginResult { Successful = false, Error = "Invalid credentials" };
        }

        public async Task<RegisterResult> Register(RegisterModel registerModel)
        {
            var result = await _httpClient.PostAsJsonAsync("api/accounts", registerModel);

            var passedresult = await result.Content.ReadFromJsonAsync<RegisterResult>();
            return passedresult;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}