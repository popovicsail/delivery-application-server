using Delivery.Domain.Entities.RestaurantEntities;
using Delivery.Domain.Interfaces;
using Delivery.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Infrastructure.Repositories;

public class RestaurantRepository : GenericRepository<Restaurant>, IRestaurantRepository
{
    public RestaurantRepository(ApplicationDbContext _dbContext) : base(_dbContext)
    {

    }

    public new async Task<Restaurant?> GetOneAsync(Guid id)
    {
        var restaurant = await _dbContext.Restaurants
            .Include(r => r.Owner)
            .Include(r => r.Address)
            .Include(r => r.BaseWorkSched)
            .Include(r => r.WorkSchedules)
            .Include(r => r.Menus)
            .Include(r => r.Workers)
            .FirstOrDefaultAsync(o => o.Id == id);

        return restaurant;
    }
}
