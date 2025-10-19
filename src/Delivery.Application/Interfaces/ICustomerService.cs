using System.Security.Claims;
using Delivery.Application.Dtos.CommonDtos.AddressDtos;
using Delivery.Application.Dtos.CommonDtos.AllergenDtos.Requests;
using Delivery.Application.Dtos.Users.CustomerDtos.Requests;
using Delivery.Application.Dtos.Users.CustomerDtos.Responses;

namespace Delivery.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerSummaryResponseDto>> GetAllAsync();
        Task<CustomerDetailResponseDto> GetOneAsync(Guid id);
        Task<CustomerDetailResponseDto> AddAsync(CustomerCreateRequestDto request);
        Task UpdateAsync(Guid id, CustomerUpdateRequestDto request);
        Task DeleteAsync(Guid id);

        Task BirthdayVoucherBackgroundJobAsync();

        Task<List<AddressDto>> GetMyAddressesAsync(ClaimsPrincipal user);
        Task CreateAddressAsync(ClaimsPrincipal user, AddressCreateRequest request);
        Task UpdateAddressAsync(ClaimsPrincipal user, Guid addressId, AddressUpdateRequest request);
        Task DeleteAddressAsync(ClaimsPrincipal user, Guid addressId);

        Task<List<Guid>> GetMyAllergensAsync(ClaimsPrincipal user);
        Task UpdateMyAllergensAsync(ClaimsPrincipal user, UpdateCustomerAllergensRequest request);
    }
}
