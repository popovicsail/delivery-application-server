using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Domain.Entities.DishEntities;
using Delivery.Domain.Entities.UserEntities;

namespace Delivery.Domain.Entities.HelperEntities
{
    public class Allergen
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public ICollection<Dish> Dishes { get; set; } = new HashSet<Dish>();

        public ICollection<Customer> Customers { get; set; } = new HashSet<Customer>();
    }
}
