using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Domain.Entities.RestaurantEntities;

namespace Delivery.Application.Dtos.OrderDtos.Responses
{
    public class OrderResponseDto
    {
        public Guid OrderId { get; set; }
        public string CustomerName { get; set; }
        public string DeliveryAddress { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public Restaurant Restaurant { get; set; }
        public int TimeToPrepare { get; set; }
        public List<OrderItemDto> Items { get; set; } = new();
        public string Status { get; set; } // npr. "KREIRANA"
    }
}
