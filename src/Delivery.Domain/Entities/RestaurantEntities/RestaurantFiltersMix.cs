using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domain.Entities.RestaurantEntities
{
    public class RestaurantFiltersMix
    {
        public string? Name { get; set; }
        public string? City { get; set; }
        public TimeSpan? OpeningTime { get; set; } = new TimeSpan(0, 0, 0);
        public TimeSpan? ClosingTime { get; set; } = new TimeSpan(23, 59, 0);
        public bool ClosedToo { get; set; } = true;
    }
}
