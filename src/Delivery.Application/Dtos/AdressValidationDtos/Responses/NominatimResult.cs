using Delivery.Application.Dtos.AdressValidationDtos.Helpers;

namespace Delivery.Application.Dtos.AdressValidationDtos.Responses
{
    public class NominatimResult
    {
        public string display_name { get; set; }
        public NominatimAddress address { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
    }
}
