using Delivery.Domain.Entities.UserEntities;
using Delivery.Domain.Interfaces;
using Delivery.Infrastructure.Persistence;

namespace Delivery.Infrastructure.Repositories;

public class CourierRepository : GenericRepository<Courier>, ICourierRepository
{
    public CourierRepository(ApplicationDbContext _dbContext) : base(_dbContext)
    {

    }
}
