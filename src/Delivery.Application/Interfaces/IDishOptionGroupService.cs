namespace Delivery.Application.Interfaces;

public interface IDishOptionGroupService
{
    Task<IEnumerable<DishOptionGroupResponseDto>> GetAllAsync();
    Task<DishOptionGroupResponseDto?> GetOneAsync(Guid id);
    Task<DishOptionGroupResponseDto> AddAsync(DishOptionGroupCreateRequestDto request);
    Task<DishOptionGroupResponseDto> UpdateAsync(Guid id, DishOptionGroupUpdateRequestDto request);
    Task DeleteAsync(Guid id);
}
