using System.Security.Claims;
using Delivery.Application.Dtos.RestaurantDtos;
using Delivery.Application.Dtos.RestaurantDtos.Requests;
using Delivery.Application.Dtos.RestaurantDtos.Responses;
using Delivery.Application.Dtos.Users.WorkerDtos.Requests;
using Delivery.Application.Dtos.Users.WorkerDtos.Responses;
using Delivery.Domain.Common;
using Delivery.Domain.Entities.RestaurantEntities;
using Microsoft.AspNetCore.Http;

namespace Delivery.Application.Interfaces;

public interface IRestaurantService
{
    Task<IEnumerable<RestaurantSummaryResponseDto>> GetAllAsync();
    Task<PaginatedList<RestaurantSummaryResponseDto>> GetPagedAsync(int sort, RestaurantFiltersMix filters, int page);
    Task<IEnumerable<RestaurantSummaryResponseDto>> GetMyRestaurantsAsync(ClaimsPrincipal User);
    Task<RestaurantDetailResponseDto?> GetOneAsync(Guid id);
    Task<RestaurantDetailResponseDto> AddAsync(RestaurantCreateRequestDto request);
    Task<RestaurantDetailResponseDto> UpdateAsync(Guid id, RestaurantUpdateRequestDto request, IFormFile? file);
    Task DeleteAsync(Guid id);
    Task<WorkerDetailResponseDto> RegisterWorkerAsync(Guid restaurantId, WorkerCreateRequestDto request, ClaimsPrincipal User);
    Task<IEnumerable<WorkerSummaryResponseDto>> GetWorkersAsync(Guid restaurantId);
    Task<MenuDto> GetRestaurantMenuAsync(Guid restaurantId);
    Task<RestaurantChangeSuspendStatusResponseDto> ChangeRestaurantSuspendStatusAsync(Guid restaurantId, RestaurantChangeSuspendStatusRequestDto request);
}
