using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domain.Entities.HelperEntities
{
    public class Address
    {
        public Guid Id { get; set; }
        public string StreetAndNumber { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }
}
