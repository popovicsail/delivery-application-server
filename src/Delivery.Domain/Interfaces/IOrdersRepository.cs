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
        Task<Order?> GetOneWithCustomerAsync(Guid orderId);
        Task<Order?> GetDraftByCustomerAsync(Guid customerId);
        Task<IEnumerable<Order>> GetByRestaurantAndDateRangeAsync(Guid restaurantId, DateTime from, DateTime to);
        Task<IEnumerable<Order>> GetByDishAndDateRangeAsync(Guid restaurantId, Guid dishId, DateTime from, DateTime to);
        Task<IEnumerable<Order>> GetCanceledByRestaurantAndDateRangeAsync(Guid restaurantId, DateTime from, DateTime to);
    }
}
