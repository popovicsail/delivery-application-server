using System.Linq.Dynamic.Core;
using Delivery.Domain.Common;
using Delivery.Domain.Interfaces;
using Delivery.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Infrastructure.Repositories
{
    public class AreasOfOperationRepository : GenericRepository<AreaOfOperation>, IAreasOfOperationRepository
    {
        public AreasOfOperationRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<bool> GetAreaConditionsByCity(string city)
        {
            IQueryable<AreaOfOperation> query = _dbContext.AreasOfOperation;

            var isWeatherGood = await query
                .Where(a => a.City == city)
                .Select(a => (bool?)a.IsWeatherGood)
                .FirstOrDefaultAsync();

            return isWeatherGood ?? true;
        }
    }
}
