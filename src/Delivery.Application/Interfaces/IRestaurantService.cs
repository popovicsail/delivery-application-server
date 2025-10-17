using Delivery.Application.Dtos.RestaurantDtos.Requests;
using Delivery.Application.Dtos.RestaurantDtos.Responses;

namespace Delivery.Application.Interfaces;

public interface IRestaurantService
{
    Task<IEnumerable<RestaurantSummaryResponseDto>> GetAllAsync();
    Task<RestaurantDetailResponseDto?> GetOneAsync(Guid id);
    Task<RestaurantDetailResponseDto> AddAsync(RestaurantCreateRequestDto request);
    Task UpdateAsync(Guid id, RestaurantUpdateRequestDto request);
    Task DeleteAsync(Guid id);
}
