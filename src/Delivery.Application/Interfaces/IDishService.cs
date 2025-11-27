using System.Security.Claims;
using Delivery.Application.Dtos.DishDtos.Requests;
using Delivery.Application.Dtos.DishDtos.Responses;
using Delivery.Application.Dtos.RestaurantDtos;
using Delivery.Domain.Common;
using Delivery.Domain.Entities.DishEntities;
using Microsoft.AspNetCore.Http;

public interface IDishService
{
    Task<PaginatedList<DishSummaryResponseDto>> GetPagedAsync(string sort, DishFiltersMix filters, int page, ClaimsPrincipal User);
    Task<IEnumerable<DishDetailResponseDto>> GetAllFilteredAsync(DishFiltersMix filters, string sort);
    Task<IEnumerable<DishDetailResponseDto>> GetAllAsync();
    Task<DishDetailResponseDto?> GetOneAsync(Guid id);
    Task<DishDetailResponseDto> AddAsync(DishCreateRequestDto request, IFormFile? file);
    Task UpdateAsync(Guid id, DishUpdateRequestDto request, IFormFile? file);
    Task DeleteAsync(Guid id);
    Task<MenuDto?> GetMenuAsync(Guid id);
}
