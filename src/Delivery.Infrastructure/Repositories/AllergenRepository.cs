using Delivery.Domain.Entities.CommonEntities;
using Delivery.Domain.Interfaces;
using Delivery.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Infrastructure.Repositories;

public class AllergenRepository : GenericRepository<Allergen>, IAllergenRepository
{
    private readonly ApplicationDbContext _dbContext;

    public AllergenRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Allergen>> FindAsync(IEnumerable<Guid> ids)
    {
        return await _dbContext.Allergens
            .Where(a => ids.Contains(a.Id))
            .ToListAsync();
    }
}
