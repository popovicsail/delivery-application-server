using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Domain.Entities.CommonEntities;
using Delivery.Domain.Entities.UserEntities;

namespace Delivery.Domain.Entities.OrderEntities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public Guid AddressId { get; set; }
        public Address Address { get; set; }

        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; } = "KREIRANA";

        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}

