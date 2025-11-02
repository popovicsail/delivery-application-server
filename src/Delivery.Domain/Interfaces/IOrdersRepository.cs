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
        Task<IEnumerable<Order>> GetByCourier(Guid courierId);
    }
}
