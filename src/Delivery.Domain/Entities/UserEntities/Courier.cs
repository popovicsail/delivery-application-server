using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Domain.Entities.HelperEntities;

namespace Delivery.Domain.Entities.UserEntities
{
    public class Courier
    {
        public Guid Id { get; set; }
        public string WorkStatus { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<WorkSchedule> WorkSchedules { get; set; } = new HashSet<WorkSchedule>();

    }
}
