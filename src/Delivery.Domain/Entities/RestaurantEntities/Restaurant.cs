using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Domain.Entities.HelperEntities;
using Delivery.Domain.Entities.UserEntities;

namespace Delivery.Domain.Entities.RestaurantEntities
{
    public class Restaurant
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Guid AddressId { get; set; }
        public virtual Address Address { get; set; }

        public Guid OwnerId { get; set; }
        public virtual Owner? Owner { get; set; }

        public virtual ICollection<Worker> Workers { get; set; } = new HashSet<Worker>();
        public virtual ICollection<WorkSchedule> WorkSchedules { get; set; } = new HashSet<WorkSchedule>();
        public virtual ICollection<Menu> Menus { get; set; } = new HashSet<Menu>();

    }
}