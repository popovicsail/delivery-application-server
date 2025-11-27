using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Domain.Entities.CommonEntities;
using Delivery.Domain.Entities.RestaurantEntities;
using Delivery.Domain.Entities.UserEntities;

namespace Delivery.Domain.Entities.OrderEntities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid? VoucherId { get; set; }
        public Voucher? Voucher { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public Guid? CourierId { get; set; }
        public Courier? Courier { get; set; }

        public Guid? AddressId { get; set; }
        public Address? Address { get; set; }

        public double TotalPrice { get; set; }
        public bool FreeDelivery { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public int? TimeToPrepare { get; set; }
        public string Status { get; set; }
        public Guid RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();

        public void SetTotalPrice()
        {
            TotalPrice = Items.Count > 0
                ? Items.Sum(i => (i.DishPrice + i.OptionsPrice) * i.Quantity - (i.DiscountExpireAt != null && i.DiscountExpireAt > DateTime.UtcNow && i.DiscountRate != 0
                ? i.DiscountRate * i.DishPrice * i.Quantity
                : 0))
                : 0;
        }
    }
}

