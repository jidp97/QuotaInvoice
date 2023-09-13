using System.Threading.Tasks;

namespace QuotaInvoice.Client.Helpers
{
    public interface IMostrarMensajes
    {
        Task MostrarMensajeError(string mensaje);

        Task MostrarMensajeExitoso(string mensaje);
    }
}