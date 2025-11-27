using Delivery.Domain.Entities.OrderEntities;
using Delivery.Domain.Interfaces;
using Delivery.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Infrastructure.Repositories
{
    public class OrderItemsRepository : GenericRepository<OrderItem>, IOrderItemsRepository
    {
        public OrderItemsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<IEnumerable<OrderItem>> GetByOrderIdAsync(Guid orderId)
        {
            return await _dbContext.OrderItems
                .Include(oi => oi.Dish)
                .Include(oi => oi.Offer)
                .Where(oi => oi.OrderId == orderId)
                .ToListAsync();
        }
    }
}

