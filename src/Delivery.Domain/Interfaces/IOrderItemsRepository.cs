using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Domain.Entities.OrderEntities;

namespace Delivery.Domain.Interfaces
{
    public interface IOrderItemsRepository : IGenericRepository<OrderItem>
    {
        // Za sada ništa specijalno
    }
}
