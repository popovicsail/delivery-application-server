using AutoMapper;
using Delivery.Application.Dtos.RestaurantDtos.Requests;
using Delivery.Application.Dtos.RestaurantDtos.Responses;
using Delivery.Application.Exceptions;
using Delivery.Application.Interfaces;
using Delivery.Domain.Entities.RestaurantEntities;
using Delivery.Domain.Interfaces;


namespace Delivery.Application.Services;

public class RestaurantService : IRestaurantService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public RestaurantService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<IEnumerable<RestaurantSummaryResponseDto>> GetAllAsync()
    {
        IEnumerable<Restaurant> restaurants = await _unitOfWork.Restaurants.GetAllAsync();

        var response = new List<RestaurantSummaryResponseDto>();

        foreach (var restaurant in restaurants)
        {
            response.Add(_mapper.Map<RestaurantSummaryResponseDto>(restaurant));
        }
        return response;
    }

    public async Task<RestaurantDetailResponseDto?> GetOneAsync(Guid id)
    {
        var restaurant = await _unitOfWork.Restaurants.GetOneAsync(id);

        if (restaurant == null)
        {
            throw new NotFoundException($"Restaurant with ID '{id}' was not found.");
        }

        var restaurantDto = _mapper.Map<RestaurantDetailResponseDto>(restaurant);

        return restaurantDto;
    }

    public async Task<RestaurantDetailResponseDto> AddAsync(RestaurantCreateRequestDto request)
    {
        var restaurant = _mapper.Map<Restaurant>(request);

        await _unitOfWork.Restaurants.AddAsync(restaurant);

        await _unitOfWork.CompleteAsync();

        return _mapper.Map<RestaurantDetailResponseDto>(restaurant);
    }

    public async Task UpdateAsync(Guid id, RestaurantUpdateRequestDto request)
    {
        var restaurant = await _unitOfWork.Restaurants.GetOneAsync(id);

        if (restaurant == null)
        {
            throw new NotFoundException($"Restaurant with ID '{id}' was not found.");
        }

        _mapper.Map(request, restaurant);

        await _unitOfWork.Restaurants.UpdateAsync(id, restaurant);

        await _unitOfWork.CompleteAsync();

        return;
    }

    public async Task DeleteAsync(Guid id)
    {
        var restaurant = await _unitOfWork.Restaurants.GetOneAsync(id);

        if (restaurant == null)
        {
            throw new NotFoundException($"Restaurant with ID '{id}' was not found.");
        }

        await _unitOfWork.Restaurants.DeleteAsync(id, restaurant);

        await _unitOfWork.CompleteAsync();

        return;
    }
}
