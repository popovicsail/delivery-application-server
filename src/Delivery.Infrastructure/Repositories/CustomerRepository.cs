using Delivery.Domain.Entities.UserEntities;
using Delivery.Domain.Interfaces;
using Delivery.Infrastructure.Persistence;

namespace Delivery.Infrastructure.Repositories;

public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(ApplicationDbContext _dbContext) : base(_dbContext)
    {

    }
}
