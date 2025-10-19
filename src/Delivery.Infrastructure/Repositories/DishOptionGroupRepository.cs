using Delivery.Domain.Entities.DishEntities;
using Delivery.Domain.Interfaces;
using Delivery.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Infrastructure.Repositories;

public class DishOptionGroupRepository : GenericRepository<DishOptionGroup>, IDishOptionGroupRepository
{
    public DishOptionGroupRepository(ApplicationDbContext _dbContext) : base(_dbContext) { }

    public new async Task<DishOptionGroup?> GetOneAsync(Guid id)
    {
        return await _dbContext.DishOptionGroups
            .Include(g => g.DishOptions)
            .FirstOrDefaultAsync(g => g.Id == id);
    }

    public new async Task<IEnumerable<DishOptionGroup>> GetAllAsync()
    {
        return await _dbContext.DishOptionGroups
            .Include(g => g.DishOptions)
            .ToListAsync();
    }
}
