using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Application.Dtos.OrderDtos.Requests
{
    public class CourierLocationDto
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
