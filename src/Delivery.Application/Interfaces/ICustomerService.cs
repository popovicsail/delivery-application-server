using Delivery.Application.Dtos.Users.CustomerDtos.Requests;
using Delivery.Application.Dtos.Users.CustomerDtos.Responses;

namespace Delivery.Application.Interfaces;

public interface ICustomerService
{
    Task<IEnumerable<CustomerSummaryResponseDto>> GetAllAsync();
    Task<CustomerDetailResponseDto?> GetOneAsync(Guid id);
    Task<CustomerDetailResponseDto> AddAsync(CustomerCreateRequestDto request);
    Task UpdateAsync(Guid id, CustomerUpdateRequestDto request);
    Task DeleteAsync(Guid id);
}
