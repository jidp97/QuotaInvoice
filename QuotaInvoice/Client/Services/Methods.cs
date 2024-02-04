using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using QuotaInvoice.Client.Services;
using System.Net.Http;
using System.Threading.Tasks;

namespace QuotaInvoice.Client.Services;

public class Methods : IMethods
{
    private readonly HttpClient _httpClient;
    private readonly HttpResponse _httpResponse;
    private readonly NavigationManager _navigationManager;
    private readonly IMatToaster _toaster;
    private readonly IJSRuntime _Js;
    private HubConnection _hubConnection;

    public Methods
    (
        HttpClient httpClient, 
        HttpResponse httpResponse, 
        NavigationManager navigationManager, 
        IMatToaster toaster,
        IJSRuntime Js
    )
    {
        _httpClient = httpClient;
        _httpResponse = httpResponse;
        _navigationManager = navigationManager;
        _toaster = toaster;
        _Js = Js;
    }
    public async Task ConnectToHub(string hubName)
    {
        _hubConnection = new HubConnectionBuilder()
            .WithUrl(_navigationManager.ToAbsoluteUri($"/{hubName}"))
            .Build();
            if(!IsConnected())
            {
                await _hubConnection.StartAsync();
            }
    }

    public async Task Edit<T>(string url, T obj, string message, string uri)
    {
        await _httpResponse.Put(url, obj);
        await _Js.SuccessNotificationAsync(message);
        _navigationManager.NavigateTo(uri);
    }

    public async Task Get<T>(string url, string Id, string uri)
    {
        var ruta = $"{url}/{Id}/{uri}";
        await _httpClient.GetAsync(ruta);
    }

    public bool IsConnected() => _hubConnection.State == HubConnectionState.Connected;

    public async Task SendMessageToHub(string messageType, string hubName)
    {
        await ConnectToHub(hubName);
        if (IsConnected())
        {
            await _hubConnection.SendAsync(messageType);
        }
        else
        {
            ShowMessage("No se ha podido conectar al servidor", "Advertencia", MatToastType.Warning);
        }
    }

    public void ShowMessage(string message, string area, MatToastType type, string icon = "")
    {
        _toaster.Add(message, type, area, icon, config =>
        {
            config.ShowCloseButton = false;
            config.ShowProgressBar = true;
            config.MaximumOpacity = 100;
            config.ShowTransitionDuration = 500;
            config.VisibleStateDuration = 5000;
            config.HideTransitionDuration = 500;
            config.RequireInteraction = false;
        });
    }
}