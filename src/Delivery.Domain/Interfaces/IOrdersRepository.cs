using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Domain.Entities.OrderEntities;
using Delivery.Domain.Entities.UserEntities;

namespace Delivery.Domain.Interfaces
{
    public interface IOrdersRepository : IGenericRepository<Order>
    {
        Task<Order?> GetOneNotDraftAsync(Guid customerId);
        Task<Order?> GetOneWithItemsAsync(Guid orderId);
        Task<IEnumerable<Order>> GetAllWithItemsAsync();
        Task<IEnumerable<Order>> GetByRestaurant(Guid restaurantId);
        Task<IEnumerable<Order>> GetByCourier(Guid courierId);
        Task<Order?> GetOneWithCustomerAsync(Guid orderId);
        Task<Order?> GetDraftByCustomerAsync(Guid customerId);
    }
}
