using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domain.Entities.DishEntities
{
    public class DishOption
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public Guid DishOptionGroupId { get; set; }
        public virtual DishOptionGroup DishOptionGroup { get; set; }
    }
}
