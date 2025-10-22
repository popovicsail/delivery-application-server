using Delivery.Application.Dtos.DishDtos.Requests;

public interface IDishService
{
    Task<IEnumerable<DishDetailResponseDto>> GetAllAsync();
    Task<DishDetailResponseDto?> GetOneAsync(Guid id);
    Task<DishDetailResponseDto> AddAsync(DishCreateRequestDto request);
    Task UpdateAsync(Guid id, DishUpdateRequestDto request);
    Task DeleteAsync(Guid id);
}
