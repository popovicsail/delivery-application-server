using Delivery.Domain.Entities.UserEntities;
using Delivery.Domain.Interfaces;
using Delivery.Infrastructure.Persistence;

namespace Delivery.Infrastructure.Repositories;

public class WorkerRepository : GenericRepository<Worker>, IWorkerRepository
{
    public WorkerRepository(ApplicationDbContext _dbContext) : base(_dbContext)
    {

    }
}
