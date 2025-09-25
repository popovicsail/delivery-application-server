using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Domain.Entities.HelperEntities;
using Delivery.Domain.Entities.RestaurantEntities;

namespace Delivery.Domain.Entities.UserEntities
{
    public class Worker
    {
        public Guid Id { get; set; }

        public bool IsSuspended { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public Guid RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public virtual ICollection<WorkSchedule> WorkSchedules { get; set; } = new HashSet<WorkSchedule>();
    }
}
