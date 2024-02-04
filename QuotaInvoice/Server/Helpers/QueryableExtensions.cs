using QuotaInvoice.Shared.Models;
using System.Linq;

namespace QuotaInvoice.Server.Helpers
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginar<T>(this IQueryable<T> queryable,
            PaginationModel pagination)
        {
            return queryable
                .Skip((pagination.Pagina - 1) * pagination.CantidadAMostrar)
                .Take(pagination.CantidadAMostrar);
        }
    }
}