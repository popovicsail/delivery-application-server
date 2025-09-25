using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domain.Entities.HelperEntities
{
    public class WorkSchedule
    {
        public Guid Id { get; set; }
        public string WeekDay { get; set; }
        public TimeSpan WorkStart { get; set; }
        public TimeSpan WorkEnd { get; set; }
    }
}
