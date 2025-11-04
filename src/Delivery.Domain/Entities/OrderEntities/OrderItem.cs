using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Domain.Entities.DishEntities;

namespace Delivery.Domain.Entities.OrderEntities
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Guid DishId { get; set; }
        public Dish? Dish { get; set; }
        public string? Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        public ICollection<DishOption> DishOptions { get; set; } = new List<DishOption>();
    }
}
