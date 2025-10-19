using Delivery.Domain.Entities.DishEntities;
using Delivery.Domain.Interfaces;
using Delivery.Infrastructure.Persistence;

namespace Delivery.Infrastructure.Repositories;

public class DishOptionGroupRepository : GenericRepository<DishOptionGroup>, IDishOptionGroupRepository
{
    public DishOptionGroupRepository(ApplicationDbContext _dbContext) : base(_dbContext)
    {

    }
}
