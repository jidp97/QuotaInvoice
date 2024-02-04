using System.Linq;
using QuotaInvoice.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace QuotaInvoice.Server.Services;

public class Services : Interfaces
{
    public ApplicationDbContext _context;
    private readonly     IHttpContextAccessor _httpContextAccessor;

    public Services
    (   
        ApplicationDbContext context,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task Add<T>(T element)
    {
        _context.Add(entity: element);
        await _context.SaveChangesAsync();
    }

    public async Task Update<T>(T element)
    {
        _context.Entry(element).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task Remove<T>(T element)
    {
        _context.Remove(element);
        await _context.SaveChangesAsync();
    }
}

  