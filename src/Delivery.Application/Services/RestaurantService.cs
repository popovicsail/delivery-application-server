using System.Security.Claims;
using AutoMapper;
using Delivery.Application.Dtos.RestaurantDtos;
using Delivery.Application.Dtos.RestaurantDtos.Requests;
using Delivery.Application.Dtos.RestaurantDtos.Responses;
using Delivery.Application.Dtos.Users.WorkerDtos.Requests;
using Delivery.Application.Dtos.Users.WorkerDtos.Responses;
using Delivery.Application.Exceptions;
using Delivery.Application.Interfaces;
using Delivery.Domain.Common;
using Delivery.Domain.Entities.CommonEntities;
using Delivery.Domain.Entities.RestaurantEntities;
using Delivery.Domain.Entities.UserEntities;
using Delivery.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;


namespace Delivery.Application.Services;

public class RestaurantService : IRestaurantService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    public RestaurantService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<IEnumerable<RestaurantSummaryResponseDto>> GetAllAsync()
    {
        var restaurants = await _unitOfWork.Restaurants.GetAllAsync();
        return _mapper.Map<List<RestaurantSummaryResponseDto>>(restaurants.ToList());
    }

    public async Task<PaginatedList<RestaurantSummaryResponseDto>> GetPagedAsync(int sort, RestaurantFiltersMix filters, int page)
    {
        if (page < 1)
        {
            page = 1;
        }
        var response = await _unitOfWork.Restaurants.GetPagedAsync(sort, filters, page);
        if (response.Items == null)
        {
            throw new NotFoundException("No restaurants found");
        }
        var mappedRestaurants = _mapper.Map<List<RestaurantSummaryResponseDto>>(response.Items);
        PaginatedList<RestaurantSummaryResponseDto> result = new PaginatedList<RestaurantSummaryResponseDto>(mappedRestaurants, response.CurrentPage, 6, response.Count);

        return result;
    }

    public async Task<RestaurantDetailResponseDto?> GetOneAsync(Guid id)
    {
        var restaurant = await _unitOfWork.Restaurants.GetOneAsync(id);

        if (restaurant == null)
        {
            throw new NotFoundException($"Restaurant with ID '{id}' was not found.");
        }

        return _mapper.Map<RestaurantDetailResponseDto>(restaurant);
    }

    public async Task<IEnumerable<RestaurantSummaryResponseDto>> GetMyRestaurantsAsync(ClaimsPrincipal User)
    {
        var user = await _userManager.GetUserAsync(User);
        var owner = await _unitOfWork.Owners.GetByUserIdAsync(user.Id);
        if (owner == null)
        {
            throw new NotFoundException($"Owner with User ID '{user.Id}' was not found.");
        }

        var restaurants = await _unitOfWork.Restaurants.GetMyAsync(owner.Id);
        return _mapper.Map<List<RestaurantSummaryResponseDto>>(restaurants);
    }

    public async Task<RestaurantDetailResponseDto> AddAsync(RestaurantCreateRequestDto request)
    {
        Guid id = Guid.NewGuid();
        var filledRestaurant = new Restaurant()
        {
            Id = id,
            Name = request.Name,
            Description = "Popuni",
            PhoneNumber = "Popuni",
            Image = "",
            Address = new Address()
            {
                StreetAndNumber = "Popuni",
                City = "Popuni",
                PostalCode = "Popuni"
            },
            OwnerId = request.OwnerId,
            BaseWorkSched = new BaseWorkSched()
            {
                Saturday = true,
                Sunday = true,
                WorkDayStart = new TimeSpan(8, 0, 0),
                WorkDayEnd = new TimeSpan(17, 0, 0),
                WeekendStart = new TimeSpan(10, 0, 0),
                WeekendEnd = new TimeSpan(16, 0, 0)
            },
            Menus = new List<Menu>
            {
                new Menu { Name = "Main Menu"}
            }
        };

        var createdRestaurant = await _unitOfWork.Restaurants.AddAsync(filledRestaurant);
        await _unitOfWork.CompleteAsync();

        return _mapper.Map<RestaurantDetailResponseDto>(filledRestaurant);
    }

    public async Task<RestaurantDetailResponseDto> UpdateAsync(Guid id, RestaurantUpdateRequestDto request, IFormFile? file)
    {
        var restaurant = await _unitOfWork.Restaurants.GetOneAsync(id);

        if (restaurant == null)
        {
            throw new NotFoundException($"Restaurant with ID '{id}' was not found.");
        }

        if (request.BaseWorkSched != null)
        {
            if (restaurant.BaseWorkSched != null)
                _mapper.Map(request.BaseWorkSched, restaurant.BaseWorkSched);
            else
                restaurant.BaseWorkSched = _mapper.Map<BaseWorkSched>(request.BaseWorkSched);
        }

        _mapper.Map(request, restaurant);

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

            restaurant.Image = await ConvertToBase64(file);
        }

        _unitOfWork.Restaurants.Update(restaurant);
        await _unitOfWork.CompleteAsync();

        return _mapper.Map<RestaurantDetailResponseDto>(restaurant);
    }

    public async Task DeleteAsync(Guid id)
    {
        var restaurant = await _unitOfWork.Restaurants.GetOneAsync(id);

        if (restaurant == null)
        {
            throw new NotFoundException($"Restaurant with ID '{id}' was not found.");
        }

        _unitOfWork.Restaurants.Delete(restaurant);
        await _unitOfWork.CompleteAsync();
    }

    public async Task<WorkerDetailResponseDto> RegisterWorkerAsync(Guid restaurantId, WorkerCreateRequestDto request, ClaimsPrincipal User)
    {
        var restaurant = await _unitOfWork.Restaurants.GetOneAsync(restaurantId);
        if (restaurant == null)
        {
            throw new NotFoundException($"Restaurant with ID '{restaurantId}' was not found.");
        }
        var user = await _userManager.GetUserAsync(User);

        var newUser = new User
        {
            UserName = request.UserName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            FirstName = request.FirstName,
            LastName = request.LastName,
            ProfilePictureBase64 = request.ProfilePictureBase64 ?? DefaultAvatar.Base64,
        };

        var result = await _userManager.CreateAsync(newUser, request.Password);
        if (!result.Succeeded)
        {
            throw new BadRequestException("ERROR: Something went wrong while creating the account.");
        }

        await _userManager.AddToRoleAsync(newUser, "Worker");

        var workerProfile = new Worker
        {
            UserId = newUser.Id,
            RestaurantId = restaurantId,
            IsSuspended = false,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            Job = request.Job
        };

        await _unitOfWork.Workers.AddAsync(workerProfile);
        await _unitOfWork.CompleteAsync();

        return _mapper.Map<WorkerDetailResponseDto>(workerProfile);
    }

    public async Task<IEnumerable<WorkerSummaryResponseDto>> GetWorkersAsync(Guid restaurantId)
    {
        var restaurant = await _unitOfWork.Restaurants.GetOneAsync(restaurantId);
        if (restaurant == null)
        {
            throw new NotFoundException($"Restaurant with ID '{restaurantId}' was not found.");
        }

        var workers = await _unitOfWork.Restaurants.GetWorkersAsync(restaurantId);
        return _mapper.Map<List<WorkerSummaryResponseDto>>(workers);
    }

    public async Task<MenuDto> GetRestaurantMenuAsync(Guid restaurantId)
    {
        var restaurant = await _unitOfWork.Restaurants.GetOneAsync(restaurantId);
        if (restaurant == null)
        {
            throw new NotFoundException($"Restaurant with ID '{restaurantId}' was not found.");
        }
        var menu = await _unitOfWork.Restaurants.GetMenuAsync(restaurantId);
        if (menu == null)
        {
            throw new NotFoundException($"Menu for Restaurant with ID '{restaurantId}' was not found.");
        }

        return _mapper.Map<MenuDto>(menu);
    }

    private static async Task<string> ConvertToBase64(IFormFile file)
    {
        using var ms = new MemoryStream();
        await file.CopyToAsync(ms);
        var fileBytes = ms.ToArray();
        return $"data:{file.ContentType};base64,{Convert.ToBase64String(fileBytes)}";
    }
}
