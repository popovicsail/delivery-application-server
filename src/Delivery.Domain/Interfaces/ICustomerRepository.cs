using Delivery.Domain.Entities.UserEntities;

namespace Delivery.Domain.Interfaces;

public interface ICustomerRepository : IGenericRepository<Customer>
{
    Task<Customer?> GetByUserIdAsync(Guid userId);
}
