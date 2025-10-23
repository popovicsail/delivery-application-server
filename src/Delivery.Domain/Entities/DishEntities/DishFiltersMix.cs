using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domain.Entities.DishEntities
{
    public class DishFiltersMix
    {
        public string? Name { get; set; }
        public string? Type { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public bool AllergicOnAlso { get; set; } = true;
        public List<Guid>? Allergens { get; set; }
    }
}
