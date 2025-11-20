using Delivery.Domain.Entities.CommonEntities;
using Delivery.Domain.Entities.RestaurantEntities;
using Delivery.Domain.Entities.UserEntities;

namespace Delivery.Domain.Entities.OrderEntities;

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

    public decimal TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; }
    public int? TimeToPrepare { get; set; }
    public string Status { get; set; }
    public Guid RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; }

    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();

    public void SetTotalPrice()
    {
        TotalPrice = (TotalPrice != 0) ? TotalPrice : (Items.Count > 0) ? Items.Sum(i => i.Price) : 0;
    }
}

