using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Application.Dtos.RestaurantDtos.Requests
{
    public class RestaurantChangeSuspendStatusRequestDto
    {
        public string? Message { get; set; }
        public bool IsSuspended { get; set; }
    }
}
