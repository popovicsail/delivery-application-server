using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Domain.Entities.RestaurantEntities;

namespace Delivery.Application.Dtos.OrderDtos.Responses
{
    public class OrderDraftResponseDto
    {
        public Guid Id { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid RestaurantId { get; set; }
        public List<OrderItemSummaryResponse> Items { get; set; } = new List<OrderItemSummaryResponse>();
    }
}
