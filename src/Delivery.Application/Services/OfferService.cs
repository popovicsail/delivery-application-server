using System.Security.Claims;
using AutoMapper;
using Delivery.Application.Dtos.OfferDtos;
using Delivery.Application.Dtos.OfferDtos.Requests;
using Delivery.Application.Dtos.OfferDtos.Responses;
using Delivery.Application.Exceptions;
using Delivery.Application.Interfaces;
using Delivery.Domain.Entities.OfferEntities;
using Delivery.Domain.Entities.UserEntities;
using Delivery.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Delivery.Application.Services
{
    public class OfferService : IOfferService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public OfferService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IEnumerable<OfferDetailsResponseDto>> GetByRestaurantAsync(Guid restaurantId, ClaimsPrincipal claimsPrincipal)
        {
            var user = await _userManager.GetUserAsync(claimsPrincipal);
            if (user == null)
            {
                throw new UnauthorizedException("User is not authorized.");
            }
            var owner = await _unitOfWork.Owners.GetByUserIdAsync(user.Id);
            if (owner == null)
            {
                throw new UnauthorizedException("Owner not found for the current user.");
            }

            var offers = await _unitOfWork.Offers.GetByRestaurantAsync(restaurantId);
            return _mapper.Map<IEnumerable<OfferDetailsResponseDto>>(offers);
        }

        public async Task<OfferDetailsResponseDto> GetOneAsync(Guid id)
        {
            var offer = await _unitOfWork.Offers.GetOneAsync(id);
            if (offer == null)
            {
                throw new NotFoundException($"Offer with ID '{id}' was not found.");
            }

            return _mapper.Map<OfferDetailsResponseDto>(offer);
        }

        public async Task<OfferDetailsResponseDto> AddAsync(Guid restaurantId, OfferCreateRequestDto request, IFormFile? file)
        {
            var restaurant = await _unitOfWork.Restaurants.GetOneAsync(restaurantId);
            if (restaurant == null)
                throw new NotFoundException($"Restaurant with ID '{restaurantId}' was not found.");

            var offer = _mapper.Map<Offer>(request);
            offer.MenuId = restaurant.Menus.FirstOrDefault()!.Id;

            const long maxFileSize = 5 * 1024 * 1024; // 5 MB

            var allowedTypes = new[] { "image/png", "image/jpeg" };

            //Picture conversion to base64
            if (file != null && file.Length > 0)
            {
                if (file.Length > maxFileSize)
                {
                    throw new Exception("File is too large. Maximum allowed size is 5 MB.");
                }
                if (!allowedTypes.Contains(file.ContentType))
                {
                    throw new Exception("Invalid file type. Only PNG and JPEG are allowed.");
                }

                offer.Image = await ConvertToBase64(file);
            }

            await _unitOfWork.Offers.AddAsync(offer);

            await _unitOfWork.CompleteAsync();

            return _mapper.Map<OfferDetailsResponseDto>(offer);
        }

        public async Task<OfferDetailsResponseDto?> UpdateAsync(Guid id, OfferUpdateRequestDto request, IFormFile? file)
        {
            var offer = await _unitOfWork.Offers.GetOneAsync(id);

            if (offer == null)
            {
                throw new NotFoundException($"Dish with ID '{id}' was not found.");
            }

            _mapper.Map(request, offer);

            const long maxFileSize = 5 * 1024 * 1024; // 5 MB

            var allowedTypes = new[] { "image/png", "image/jpeg" };

            //Picture conversion to base64
            if (file != null && file.Length > 0)
            {
                if (file.Length > maxFileSize)
                {
                    throw new Exception("File is too large. Maximum allowed size is 5 MB.");
                }
                if (!allowedTypes.Contains(file.ContentType))
                {
                    throw new Exception("Invalid file type. Only PNG and JPEG are allowed.");
                }

                offer.Image = await ConvertToBase64(file);
            }

            _unitOfWork.Offers.Update(offer);

            await _unitOfWork.CompleteAsync();
            return offer == null ? null : _mapper.Map<OfferDetailsResponseDto>(offer);
        }

        public async Task ManageItemsInOfferAsync(Guid offerId, List<OfferDishDto> dtos)
        {
            var offer = await _unitOfWork.Offers.GetOneAsync(offerId);
            if (offer == null)
                throw new NotFoundException($"Offer with ID '{offerId}' was not found.");

            var dishIds = dtos.Select(d => d.DishId).ToList();
            var existingDishIds = await _unitOfWork.Dishes.GetManyIdsAsync(dishIds);

            var missing = dishIds.Except(existingDishIds).ToList();
            if (missing.Any())
                throw new NotFoundException($"Dishes not found: {string.Join(", ", missing)}");

            var offerDishes = _mapper.Map<List<OfferDish>>(dtos);

            offer.OfferDishes = offerDishes;

            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var offer = await _unitOfWork.Offers.GetOneAsync(id);

            if (offer == null)
            {
                throw new NotFoundException($"Offer with ID '{id}' was not found.");
            }

            _unitOfWork.Offers.Delete(offer);

            await _unitOfWork.CompleteAsync();
        }

        private static async Task<string> ConvertToBase64(IFormFile file)
        {
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            var fileBytes = ms.ToArray();
            return $"data:{file.ContentType};base64,{Convert.ToBase64String(fileBytes)}";
        }
    }
}
