using Delivery.Domain.Entities.OrderEntities;

namespace Delivery.Domain.Interfaces
{
    public interface IBillRepository
    {
        Task CreateAsync(Bill bill);

        Task<Bill> GetByOrderIdAsync(Guid orderId);
    }
}