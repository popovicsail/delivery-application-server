using Delivery.Domain.Entities.UserEntities;
using Delivery.Domain.Interfaces;
using Delivery.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Infrastructure.Repositories;

public class VoucherRepository : GenericRepository<Voucher>, IVoucherRepository
{
    public VoucherRepository(ApplicationDbContext _dbContext) : base(_dbContext)
    {
        

    }

    public async Task<IEnumerable<Voucher>> GetVouchersByCustomerId(Guid customerId)
    {
        IQueryable<Voucher> query = _dbContext.Vouchers;

        query = query
                .Where(v => v.CustomerId == customerId);

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Voucher>> GetActiveVouchersAsync()
    {
        IQueryable<Voucher> query = _dbContext.Vouchers;

        query = query
            .Where(v => v.Status == "Active")
            .Where(v => v.ExpirationDate < DateTime.UtcNow);

        return await query.ToListAsync();
    }
}
