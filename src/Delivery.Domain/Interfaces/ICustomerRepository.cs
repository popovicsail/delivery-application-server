using Delivery.Domain.Entities.UserEntities;

namespace Delivery.Domain.Interfaces;

public interface ICustomerRepository : IGenericRepository<Customer>
{
    Task<IEnumerable<Customer?>> GetBirthdayCustomersAsync();
    public Task<bool> HasReceivedBirthdayVoucherInLastYearAsync(Guid customerId);
}
