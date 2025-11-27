using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Application.Dtos.AdressValidationDtos.Responses
{
    public class AddressValidationResultDto
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }

        public static AddressValidationResultDto Valid() => new() { IsValid = true };
        public static AddressValidationResultDto Invalid(string msg) => new() { IsValid = false, Message = msg };
    }


}
