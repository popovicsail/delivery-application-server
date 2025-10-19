using Delivery.Domain.Entities.UserEntities;
using Delivery.Domain.Interfaces;
using Delivery.Infrastructure.Persistence;

namespace Delivery.Infrastructure.Repositories;

public class AdministratorRepository : GenericRepository<Administrator>, IAdministratorRepository
{
    public AdministratorRepository(ApplicationDbContext _dbContext) : base(_dbContext)
    {

    }
}
