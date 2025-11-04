using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Application.Dtos.CommonDtos.AddressDtos;
using Delivery.Application.Dtos.OrderDtos.Responses;
using Delivery.Domain.Entities.CommonEntities;

namespace Delivery.Application.Dtos.OrderDtos.Requests
{
    public class CreateOrderItemsDto
    {
        public Guid CustomerId { get; set; }
        public Guid RestaurantId { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }
}
