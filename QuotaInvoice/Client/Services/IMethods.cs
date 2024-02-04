using MatBlazor;

namespace QuotaInvoice.Client.Services;

public interface IMethods
{
    public Task Edit<T>(string url, T obj, string message, string uri);
    public Task Get<T>(string url, string Id, string uri);
    public void ShowMessage(string message, string area, MatToastType type, string icon = "");
    public Task ConnectToHub(string hubname);
    public bool IsConnected();
    public Task SendMessageToHub(string messageType, string hubName);
}

