using System.Security.Claims;
using Delivery.Application.Dtos.DishDtos.Requests;
using Delivery.Application.Dtos.DishDtos.Responses;
using Delivery.Application.Dtos.RestaurantDtos;
using Delivery.Domain.Common;
using Delivery.Domain.Entities.DishEntities;

public interface IDishService
{
    Task<PaginatedList<DishSummaryResponseDto>> GetPagedAsync(int sort, DishFiltersMix filters, int page, ClaimsPrincipal User);
    Task<IEnumerable<DishDetailResponseDto>> GetAllAsync();  // promenjeno
    Task<DishDetailResponseDto?> GetOneAsync(Guid id);
    Task<DishDetailResponseDto> AddAsync(DishCreateRequestDto request);
    Task UpdateAsync(Guid id, DishUpdateRequestDto request);
    Task DeleteAsync(Guid id);
    Task<MenuDto?> GetMenuAsync(Guid id);
}
