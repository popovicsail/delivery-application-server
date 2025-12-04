namespace Delivery.Application.Dtos.OrderDtos.Requests
{
    public class OrderStatusUpdateRequestDto
    {
        public Guid OrderId { get; set; }
        public string NewStatus { get; set; }
        public int PrepTime { get; set; }
        public DateTime EstimatedReadyAt { get; set; }
        public int? DeliveryTimeMinutes { get; set; }
        public DateTime? EstimatedDeliveryAt { get; set; }
        public string? DeliveryEstimateMessage { get; set; }

    }
}
