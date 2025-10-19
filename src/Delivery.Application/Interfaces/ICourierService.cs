using Delivery.Application.Dtos.Users.CourierDtos.Requests;
using Delivery.Application.Dtos.Users.CourierDtos.Responses;

namespace Delivery.Application.Interfaces;

public interface ICourierService
{
    Task<IEnumerable<CourierSummaryResponseDto>> GetAllAsync();
    Task<CourierDetailResponseDto?> GetOneAsync(Guid id);
    Task<CourierDetailResponseDto> AddAsync(CourierCreateRequestDto request);
    Task UpdateAsync(Guid id, CourierUpdateRequestDto request);
    Task DeleteAsync(Guid id);
}
