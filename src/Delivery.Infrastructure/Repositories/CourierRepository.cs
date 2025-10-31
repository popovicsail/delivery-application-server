using Microsoft.EntityFrameworkCore;
using Delivery.Domain.Entities.UserEntities;
using Delivery.Domain.Interfaces;
using Delivery.Infrastructure.Persistence;

namespace Delivery.Infrastructure.Repositories;

public class CourierRepository : GenericRepository<Courier>, ICourierRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CourierRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Courier?> GetOneWithUserAsync(Guid userId)
    {
        return await _dbContext.Couriers
            .Include(c => c.User)
            .Include(c => c.WorkSchedules)
            .FirstOrDefaultAsync(c => c.UserId == userId);
    }

    public async Task<List<Courier>> GetAllWithSchedulesAsync()
    {
        return await _dbContext.Couriers
            .Include(c => c.User)
            .Include(c => c.WorkSchedules)
            .ToListAsync();
    }

    public async Task<IEnumerable<Courier>> GetAllWithOrdersAsync()
    {
        return await _dbContext.Couriers
            .Include(c => c.Orders)
            .ToListAsync();
    }

}
