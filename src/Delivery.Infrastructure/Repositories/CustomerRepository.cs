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

    public async Task<IEnumerable<Customer>> GetBirthdayCustomersAsync()
    {
        var today = DateTime.UtcNow;

        IQueryable<Customer> query = _dbContext.Customers;

        query = query
                .Include(c => c.User);

        query = query
                .Where(c => c.User != null &&
                            c.User.DateOfBirth.HasValue &&
                            c.User.DateOfBirth.Value.Day == today.Day &&
                            c.User.DateOfBirth.Value.Month == today.Month);

        return await query.ToListAsync();
    }

    public async Task<bool> HasReceivedBirthdayVoucherInLastYearAsync(Guid customerId)
    {
        var oneYearAgo = DateTime.UtcNow.AddYears(-1);

        return await _dbContext.Vouchers.AnyAsync(v =>
            v.CustomerId == customerId &&
            v.Name == "Birthday Voucher" &&
            v.DateIssued >= oneYearAgo
        );
    }
}
