using Delivery.Domain.Entities.OrderEntities;

namespace Delivery.Domain.Interfaces
{
    public interface IOrdersRepository : IGenericRepository<Order>
    {
        Task<Order?> GetOneNotDraftAsync(Guid customerId);
        Task<Order?> GetOneWithItemsAsync(Guid orderId);
        Task<IEnumerable<Order>> GetAllWithItemsAsync();
        IQueryable<Order> GetByRestaurant(Guid restaurantId,DateTime? from = null,DateTime? to = null);
        IQueryable<Order> GetByCourier(
        Guid courierId,
        DateTime? from = null,
        DateTime? to = null);
        IQueryable<Order> GetByCustomer(Guid customerId);

        Task<Order?> GetOneWithCustomerAsync(Guid orderId);
        Task<Order?> GetDraftByCustomerAsync(Guid customerId);
        Task<IEnumerable<Order>> GetByRestaurantAndDateRangeAsync(Guid restaurantId, DateTime from, DateTime to);
        Task<IEnumerable<Order>> GetByDishAndDateRangeAsync(Guid dishId, DateTime from, DateTime to);
        Task<IEnumerable<Order>> GetCanceledByRestaurantAndDateRangeAsync(Guid restaurantId, DateTime from, DateTime to);
    }
}