using Delivery.Application.Dtos.DishDtos.Requests;
using Delivery.Application.Dtos.DishDtos.Responses;

namespace Delivery.Application.Interfaces;

public interface IDishService
{
    Task<IEnumerable<DishSummaryResponseDto>> GetAllAsync();
    Task<DishDetailResponseDto?> GetOneAsync(Guid id);
    Task<DishDetailResponseDto> AddAsync(DishCreateRequestDto request);
    Task UpdateAsync(Guid id, DishUpdateRequestDto request);
    Task DeleteAsync(Guid id);
}
