using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotaInvoice.Shared.Models
{
    public class HubMessagesType
    {
        public const string Modificado = "MessageEdit";
        public const string Eliminado = "MessageDelete";
        public const string Agregado = "SendMessage";
    }
}
