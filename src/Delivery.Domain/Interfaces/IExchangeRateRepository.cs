using Delivery.Domain.Common;

namespace Delivery.Domain.Interfaces
{
    public interface IExchangeRateRepository : IGenericRepository<ExchangeRate>
    {
        Task<ExchangeRate?> GetByBaseCodeAsync(string baseCode);
    }
}