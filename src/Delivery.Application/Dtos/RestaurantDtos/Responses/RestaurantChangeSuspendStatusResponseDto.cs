using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Application.Dtos.RestaurantDtos.Responses
{
    public class RestaurantChangeSuspendStatusResponseDto
    {
        public Guid RestaurantId { get; set; }
        public bool IsSuspended { get; set; }
    }
}
