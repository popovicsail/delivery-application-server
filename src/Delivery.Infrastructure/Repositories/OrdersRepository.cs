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

        public async Task<IEnumerable<Order>> GetByCourier(
        Guid courierId,
        DateTime? from = null,
        DateTime? to = null,
        int page = 1,
        int pageSize = 10)
        {
            var query = _dbContext.Orders
                .Where(o => o.CourierId == courierId)
                .Include(o => o.Items).ThenInclude(i => i.Dish)
                .Include(o => o.Customer).ThenInclude(c => c.User)
                .Include(o => o.Address)
                .Include(o => o.Restaurant).ThenInclude(r => r.Address)
                .AsQueryable();

            if (from.HasValue)
                query = query.Where(o => o.CreatedAt >= from.Value.ToUniversalTime());

            if (to.HasValue)
                query = query.Where(o => o.CreatedAt <= to.Value.ToUniversalTime());


            return await query
                .OrderByDescending(o => o.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetByCustomer(
        Guid customerId,
        int page = 1,
        int pageSize = 10)
        {
            var query = _dbContext.Orders
                .Where(o => o.CustomerId == customerId)
                .Include(o => o.Items).ThenInclude(i => i.Dish)
                .Include(o => o.Restaurant).ThenInclude(r => r.Address)
                .Include(o => o.Address)
                .OrderByDescending(o => o.CreatedAt);

            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
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
                .Include(o => o.Restaurant)
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
