using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

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