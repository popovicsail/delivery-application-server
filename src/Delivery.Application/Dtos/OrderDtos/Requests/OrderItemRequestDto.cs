using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Application.Dtos.OrderDtos.Requests
{
    public class OrderItemRequestDto
    {
        public Guid DishId { get; set; }
        public int Quantity { get; set; }
    }
}
