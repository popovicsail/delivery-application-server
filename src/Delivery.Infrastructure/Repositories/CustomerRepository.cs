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

    public new async Task<Customer?> GetOneAsync(Guid userId)
    {
        return await _dbContext.Customers
            .Include(c => c.User)
            .Include(c => c.Addresses)
            .Include(c => c.Allergens)
            .Include(c => c.Vouchers)
            .FirstOrDefaultAsync(c => c.UserId == userId);
    }

}
