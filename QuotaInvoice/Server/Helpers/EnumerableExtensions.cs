using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QuotaInvoice.Server.Helpers
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<TSource> GetAll<TSource>(this IEnumerable<TSource> source)
        {
            return Enumerable.ToList(source);
        }

        public static Task<List<TSource>> GetAllAsync<TSource>([NotNullAttribute] this IQueryable<TSource> source, CancellationToken cancellationToken = default)
        {
            return source
                .OrderByDescending(x => x).ToListAsync();
        }
    }
}