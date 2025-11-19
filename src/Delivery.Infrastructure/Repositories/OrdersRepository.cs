using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Domain.Entities.OrderEntities;
using Delivery.Domain.Interfaces;
using Delivery.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Infrastructure.Repositories
{
    public class OrdersRepository : GenericRepository<Order>, IOrdersRepository
    {
        public OrdersRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<Order>> GetByRestaurant(Guid restaurantId)
        {
            return await _dbContext.Orders
                .Where(o => o.RestaurantId == restaurantId)
                .Include(o => o.Items)
                    .ThenInclude(i => i.Dish)
                .Include(o => o.Customer)
                    .ThenInclude(c => c.User)
                .Include(o => o.Address)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetByCourier(Guid courierId)
        {
            return await _dbContext.Orders
                .Where(o => o.CourierId == courierId)
                .Include(o => o.Items)
                    .ThenInclude(i => i.Dish)
                .Include(o => o.Customer)
                    .ThenInclude(c => c.User)
                .Include(o => o.Address)
                .Include(o => o.Restaurant)
                    .ThenInclude(r => r.Address)
                .ToListAsync();
        }

        public async Task<Order?> GetOneNotDraftAsync(Guid customerId)
        {
            return await _dbContext.Orders
                .Where(o => o.CustomerId == customerId && (o.Status != "Draft"))
                .Include(o => o.Items)
                    .ThenInclude(i => i.Dish)
                .Include(o => o.Customer)
                    .ThenInclude(c => c.User)
                .Include(o => o.Address)
                .FirstOrDefaultAsync();
        }

        public async Task<Order?> GetDraftByCustomerAsync(Guid customerId)
        {
            return await _dbContext.Orders
                .Where(o => o.CustomerId == customerId && (o.Status == "Draft"))
                .Include(o => o.Items)
                    .ThenInclude(i => i.Dish)
                .Include(o => o.Customer)
                    .ThenInclude(c => c.User)
                .Include(o => o.Address)
                .FirstOrDefaultAsync();
        }

        public async Task<Order?> GetOneWithItemsAsync(Guid orderId)
        {
            return await _dbContext.Orders
                .Include(o => o.Items)
                    .ThenInclude(i => i.Dish)
                .Include(o => o.Customer)
                    .ThenInclude(c => c.User)
                .Include(o => o.Address)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }


        public async Task<Order?> GetOneWithCustomerAsync(Guid orderId)
        {
            return await _dbContext.Orders
                .Include(o => o.Items)
                    .ThenInclude(i => i.Dish)
                .Include(o => o.Customer)
                    .ThenInclude(c => c.User)
                .Include(o => o.Customer)
                    .ThenInclude(c => c.Addresses)
                .Include(o => o.Customer)
                    .ThenInclude(c => c.Vouchers)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<IEnumerable<Order>> GetAllWithItemsAsync()
        {
            return await _dbContext.Orders
                .Include(o => o.Items)
                    .ThenInclude(i => i.Dish)
                .Include(o => o.Customer)
                    .ThenInclude(c => c.User)
                .Include(o => o.Address)
                .ToListAsync();
        }
    }
}
