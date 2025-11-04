using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Application.Dtos.OrderDtos.Requests
{
    public class UpdateOrderDetailsDto
    {
        public Guid AddressId { get; set; }
        public Guid? VoucherId { get; set; }
        public string? Note { get; set; }
    }
}
