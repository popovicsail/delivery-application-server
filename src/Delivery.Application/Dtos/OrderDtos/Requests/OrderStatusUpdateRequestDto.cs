using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Application.Dtos.OrderDtos.Requests
{
    public class OrderStatusUpdateRequestDto
    {
        public int NewStatus { get; set; }
        public int PrepTime { get; set; }
    }
}
