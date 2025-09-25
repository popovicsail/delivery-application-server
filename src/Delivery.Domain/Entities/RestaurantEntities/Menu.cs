using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Domain.Entities.DishEntities;

namespace Delivery.Domain.Entities.RestaurantEntities
{
    public class Menu
    {
        public Guid Id { get; set; }

        public Guid RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }

        public virtual ICollection<Dish> Dishes { get; set; } = new HashSet<Dish>();
    }
}
