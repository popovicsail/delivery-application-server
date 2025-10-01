using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domain.Entities.DishEntities
{
    public class DishOptionGroup
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
      
        public Guid DishId { get; set; }
        public virtual Dish Dish{ get; set; }

        public virtual ICollection<DishOption> DishOptions { get; set; } = new HashSet<DishOption>();
    }
}
