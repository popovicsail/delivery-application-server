using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Application.Dtos.OrderDtos.Requests
{
    public class AssignOrderRequestDto
    {
        public Guid OrderId { get; set; }
        public Guid CourierId { get; set; }
    }
}
