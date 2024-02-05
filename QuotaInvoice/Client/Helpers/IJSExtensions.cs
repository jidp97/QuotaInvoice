using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace QuotaInvoice.Client.Helpers;

public static class IJSExtensions
{
    public static async Task<object> MostrarMensajeAsync(this IJSRuntime js, string mensaje) =>
         await js.InvokeAsync<object>("Swal.fire", mensaje);


    public static async Task<object> MostrarMensajeAsync(this IJSRuntime js, string titulo, string mensaje, TipoMensajeSweetAlert TipoMensaje) 
        => await js.InvokeAsync<object>("Swal.fire", titulo, mensaje, TipoMensaje.ToString());

    public static async Task<bool> ConfirmAsync(this IJSRuntime js, string titulo, string mensaje, TipoMensajeSweetAlert tipoMensaje) 
        => await js.InvokeAsync<bool>("customConfirm", titulo, mensaje, tipoMensaje.ToString());

    public static async Task<bool> ConfirmMessageAsync(this IJSRuntime js, string titulo, string mensaje, string resultTitulo, string resultMessage, TipoMensajeSweetAlert tipoMensaje) 
        => await js.InvokeAsync<bool>("confirmMessage", titulo, mensaje, resultTitulo, resultMessage, tipoMensaje.ToString());

    public static async Task<object> LoadingAsync(this IJSRuntime js) => await js.InvokeAsync<object>("loading");

    public static async Task<object> RemoveLoadingAsync(this IJSRuntime js) => await js.InvokeAsync<object>("removeLoading");

    public static async Task<object> EmptyDbNotifyAsync(this IJSRuntime js) => await js.InvokeAsync<object>("emptyDbNotify");
    public static async Task<object> WelcomeAsync(this IJSRuntime js) => await js.InvokeAsync<object>("Welcome");

}

public enum TipoMensajeSweetAlert
{
    question, warning, error, success, info
}