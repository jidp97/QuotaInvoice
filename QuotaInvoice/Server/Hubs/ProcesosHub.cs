using Microsoft.AspNetCore.SignalR;

namespace QuotaInvoice.Server.Hubs
{
    public class ProcesosHub : Hub
    {
        public async Task SendMessage()
        {
            await Clients.Others.SendAsync("RecieveMessage");
        }

        public async Task MessageEdit()
        {
            await Clients.Others.SendAsync("Editado");
        }

        public async Task MessageDelete()
        {
            await Clients.Others.SendAsync("Eliminado");
        }
    }
}