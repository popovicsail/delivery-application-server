using Delivery.Domain.Entities.UserEntities;
using Delivery.Domain.Interfaces;
using Delivery.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Infrastructure.Repositories;

public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
{
    private readonly ApplicationDbContext _dbContext;
    public CustomerRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Customer?> GetByUserIdAsync(Guid userId)
    {
        return await _dbContext.Customers
            .Include(c => c.Addresses)
            .Include(c => c.Allergens)
            .FirstOrDefaultAsync(c => c.UserId == userId);
    }

}
