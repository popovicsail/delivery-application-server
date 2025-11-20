using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Domain.Entities.OrderEntities;

namespace Delivery.Domain.Interfaces
{
    public interface IOrdersRepository : IGenericRepository<Order>
    {
        Task<Order?> GetOneWithItemsAsync(Guid orderId);
        Task<IEnumerable<Order>> GetAllWithItemsAsync();
        Task<IEnumerable<Order>> GetByRestaurant(Guid restaurantId);
        IQueryable<Order> GetByCourier(
        Guid courierId,
        DateTime? from = null,
        DateTime? to = null);
        IQueryable<Order> GetByCustomer(Guid customerId);

        Task<Order?> GetOneWithCustomerAsync(Guid orderId);
        Task<Order?> GetDraftByCustomerAsync(Guid customerId);
    }
}
