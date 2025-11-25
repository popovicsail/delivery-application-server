using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Application.Dtos.AdressValidationDtos.Helpers;
using Delivery.Infrastructure.Services;

namespace Delivery.Application.Dtos.AdressValidationDtos.Responses
{
    public class NominatimResult
    {
        public string display_name { get; set; }
        public NominatimAddress address { get; set; }
    }
}
