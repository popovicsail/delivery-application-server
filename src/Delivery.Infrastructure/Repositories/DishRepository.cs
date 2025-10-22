using Delivery.Domain.Entities.DishEntities;
using Delivery.Domain.Interfaces;
using Delivery.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Infrastructure.Repositories;

public class DishRepository : GenericRepository<Dish>, IDishRepository
{
    public DishRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    public new async Task<IEnumerable<Dish>> GetAllAsync()
    {
        return await _dbContext.Dishes
            .Include(d => d.DishOptionGroups)
                .ThenInclude(g => g.DishOptions) // Include sve dish options
            .Include(d => d.Allergens) // ako želiš i alergene
            .ToListAsync();
    }

    public new async Task<Dish?> GetOneAsync(Guid id)
    {
        return await _dbContext.Dishes
            .Include(d => d.DishOptionGroups)
                .ThenInclude(g => g.DishOptions)
            .Include(d => d.Allergens)
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<IEnumerable<Dish>> GetByIdsWithAllergensAsync(IEnumerable<Guid> dishIds)
    {
        return await _dbContext.Dishes
            .Include(d => d.Allergens)
            .Include(d => d.DishOptionGroups)
                .ThenInclude(g => g.DishOptions)
            .Where(d => dishIds.Contains(d.Id))
            .ToListAsync();
    }
}
