using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Delivery.Application.Dtos.OfferDtos;
using Delivery.Application.Dtos.OfferDtos.Requests;
using Delivery.Application.Dtos.OfferDtos.Responses;
using Microsoft.AspNetCore.Http;

namespace Delivery.Application.Interfaces
{
    public interface IOfferService
    {
        Task<IEnumerable<OfferDetailsResponseDto>> GetByRestaurantAsync(Guid restaurantId, ClaimsPrincipal claimsPrincipal);
        Task<OfferDetailsResponseDto> GetOneAsync(Guid id);
        Task<OfferDetailsResponseDto> AddAsync(Guid restaurantId, OfferCreateRequestDto request, IFormFile? file);
        Task<OfferDetailsResponseDto?> UpdateAsync(Guid id, OfferUpdateRequestDto request, IFormFile? file);
        Task ManageItemsInOfferAsync(Guid offerId, List<OfferDishDto> dtos);
        Task DeleteAsync(Guid id);
    }
}
