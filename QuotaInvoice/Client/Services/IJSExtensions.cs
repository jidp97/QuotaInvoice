using Microsoft.JSInterop;

namespace QuotaInvoice.Client.Services
{
    public static class IJSExtensions
    {

        public static async Task<object> SuccessNotificationAsync(this IJSRuntime js, string message)
        {
            return js.InvokeAsync<object>("NotiflixSucess", message);
        }

        public static async Task<object> WarningNotificationAsync(this IJSRuntime js, string message)
        {
            return js.InvokeAsync<object>("NotiflixWarning", message);
        }

    }
}