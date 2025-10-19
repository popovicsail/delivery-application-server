using Delivery.Application.Dtos.CommonDtos.AllergenDtos.Requests;
using Delivery.Application.Dtos.CommonDtos.AllergenDtos.Responses;

namespace Delivery.Application.Interfaces;

public interface IAllergenService
{
    Task<IEnumerable<AllergenSummaryResponseDto>> GetAllAsync();
    Task<AllergenDetailResponseDto?> GetOneAsync(Guid id);
    Task<AllergenDetailResponseDto> AddAsync(AllergenCreateRequestDto request);
    Task UpdateAsync(Guid id, AllergenUpdateRequestDto request);
    Task DeleteAsync(Guid id);
}
