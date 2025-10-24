using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Application.Dtos.Users.CourierDtos.Requests
{
    public class WorkScheduleUpdateRequestDto
    {
        public string Date { get; set; }
        public string WeekDay { get; set; }   // npr. "Monday"
        public TimeSpan WorkStart { get; set; }
        public TimeSpan WorkEnd { get; set; }
    }
}
