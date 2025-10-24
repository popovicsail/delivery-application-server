using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Application.Dtos.CommonDtos.AddressDtos;
using Delivery.Domain.Entities.CommonEntities;

namespace Delivery.Application.Dtos.OrderDtos.Requests
{
    public class CreateOrderRequestDto
    {
        public Guid CustomerId { get; set; }
        public Guid AddressId { get; set; }
        public List<OrderItemRequestDto> Items { get; set; } = new();
    }
}
