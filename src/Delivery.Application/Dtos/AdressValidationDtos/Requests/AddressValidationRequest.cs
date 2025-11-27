using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Application.Dtos.AdressValidationDtos.Requests
{
    public class AddressValidationRequest
    {
        public string Address { get; set; }
        public string RestaurantCity { get; set; }
    }

}
