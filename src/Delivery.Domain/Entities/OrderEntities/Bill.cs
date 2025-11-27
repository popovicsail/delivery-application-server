namespace Delivery.Domain.Entities.OrderEntities
{
    public class Bill
    {
        public string Id { get; set; }

        public Guid OrderId { get; set; }

        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }

        public DateTime IssuedAt { get; set; }
        public decimal TotalAmount { get; set; }

        public List<BillItem> Items { get; set; } = new List<BillItem>();
    }

    public class BillItem
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice => Quantity * Price;
    }
}