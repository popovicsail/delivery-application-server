namespace Delivery.Application.Dtos.OrderDtos.Responses;

namespace Delivery.Application.Dtos.OrderDtos.Responses
{
    public class OrderDraftResponseDto
    {
        public Guid Id { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
        public List<OrderItemSummaryResponse> Items { get; set; } = new List<OrderItemSummaryResponse>();
    }
}
