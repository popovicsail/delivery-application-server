using System.Threading.Tasks;
using Delivery.Application.Dtos.AdressValidationDtos.Responses;

namespace Delivery.Application.Interfaces
{
    public interface IAddressValidationService
    {
        Task<AddressValidationResultDto> ValidateAsync(string address, string restaurantCity);
    }
}
