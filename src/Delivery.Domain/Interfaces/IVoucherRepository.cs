using Delivery.Domain.Entities.UserEntities;

namespace Delivery.Domain.Interfaces;

public interface IVoucherRepository : IGenericRepository<Voucher>
{
    Task<IEnumerable<Voucher>> GetVouchersByCustomerId(Guid customerId);

    Task<IEnumerable<Voucher>> GetActiveVouchersAsync();

}
