using System.Threading.Tasks;

namespace QuotaInvoice.Server.Services;

public interface Interfaces
{
    public Task Add<T>(T element);
    public Task Update<T>(T element);
    public Task Remove<T>(T element);
}

