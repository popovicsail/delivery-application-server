using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domain.Entities.HelperEntities
{
    public class Allergen
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
