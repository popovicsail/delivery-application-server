using Delivery.Domain.Common;
using Delivery.Domain.Interfaces;
using Delivery.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Infrastructure.Repositories
{
    public class ExchangeRateRepository : GenericRepository<ExchangeRate>, IExchangeRateRepository
    {
        public ExchangeRateRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<ExchangeRate?> GetByBaseCodeAsync(string baseCode)
        {
            var query = _dbContext.ExchangeRates;

            var exchangeRate = await query    
                .FirstOrDefaultAsync(e => e.BaseCode == baseCode);

            return exchangeRate;
        }
    }
}