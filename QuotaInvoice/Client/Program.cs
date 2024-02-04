using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using QuotaInvoice.Client;
using MudBlazor.Services;
using Blazored.LocalStorage;
using QuotaInvoice.Shared.Models;
using Microsoft.AspNetCore.Components.Authorization;
using QuotaInvoice.Client.Services;
using QuotaInvoice.Client.Helpers;
using MatBlazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore(config =>
{
    config.AddPolicy(Policies.IsAdmin, Policies.IsAdminPolicy());
    config.AddPolicy(Policies.IsUser, Policies.IsUserPolicy());
});
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IHttpResponse, HttpResponse>();
builder.Services.AddScoped<IMethods, Methods>();
builder.Services.AddScoped<IMostrarMensajes, MostrarMensajes>();
builder.Services.AddHttpClient("QuotaInvoice.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("QuotaInvoice.ServerAPI"));
builder.Services.AddMudServices();
builder.Services.AddMatToaster(config =>
{
    config.Position = MatToastPosition.BottomRight;
    config.PreventDuplicates = true;
    config.NewestOnTop = true;
    config.ShowCloseButton = true;
    config.MaximumOpacity = 95;
    config.VisibleStateDuration = 3000;
});
await builder.Build().RunAsync();
