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
