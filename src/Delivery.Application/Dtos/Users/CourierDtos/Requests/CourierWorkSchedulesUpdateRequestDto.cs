using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Application.Dtos.Users.CourierDtos.Requests
{
    public class CourierWorkSchedulesUpdateRequestDto
    {
        public List<WorkScheduleUpdateRequestDto> Schedules { get; set; } = new();
    }
}
