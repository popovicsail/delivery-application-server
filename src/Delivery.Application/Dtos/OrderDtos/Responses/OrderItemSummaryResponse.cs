namespace Delivery.Application.Dtos.OrderDtos.Responses
{
    public class OrderItemSummaryResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int Quantity { get; set; }
        public string? ItemType { get; set; }
        public double DishPrice { get; set; }
        public double OptionsPrice { get; set; }
        public double DiscountRate { get; set; } = 0;
        public DateTime? DiscountExpireAt { get; set; }
        public List<DishOptionDto> DishOptions { get; set; } = new List<DishOptionDto>();
    }
}