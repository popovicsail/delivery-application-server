using System.Security.Claims;
using AutoMapper;
using Delivery.Application.Dtos.DishDtos.Requests;
using Delivery.Application.Dtos.DishDtos.Responses;
using Delivery.Application.Dtos.RestaurantDtos;
using Delivery.Application.Exceptions;
using Delivery.Application.Interfaces;
using Delivery.Domain.Common;
using Delivery.Domain.Entities.DishEntities;
using Delivery.Domain.Interfaces;
using Microsoft.AspNetCore.Http;


namespace Delivery.Application.Services;

public class DishService : IDishService
{
    private readonly ICustomerService _customerService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public DishService(IUnitOfWork unitOfWork, IMapper mapper, ICustomerService customerService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _customerService = customerService;
    }
    public async Task<IEnumerable<DishDetailResponseDto>> GetAllAsync()
    {
        var dishes = await _unitOfWork.Dishes.GetAllAsync();
        return _mapper.Map<IEnumerable<DishDetailResponseDto>>(dishes);
    }

    public async Task<IEnumerable<DishDetailResponseDto>> GetAllFilteredAsync(DishFiltersMix filters, string sort)
    {
        var dishes = await _unitOfWork.Dishes.GetAllFilteredAsync(filters, sort);
        return _mapper.Map<IEnumerable<DishDetailResponseDto>>(dishes);
    }

    public async Task<PaginatedList<DishSummaryResponseDto>> GetPagedAsync(string sort, DishFiltersMix filters, int page, ClaimsPrincipal User)
    {
        if (page < 1)
        {
            page = 1;
        }

        if (User.IsInRole("Customer") && filters.AllergicOnAlso != null && !filters.AllergicOnAlso)
        {
            var userAllergens = await _customerService.GetMyAllergensAsync(User);
            filters.Allergens = userAllergens;
        }
        
        var response = await _unitOfWork.Dishes.GetPagedAsync(sort, filters, page);
        if (response.Items == null)
        {
            throw new NotFoundException("No dishes found");
        }
        var mappedDishes = _mapper.Map<List<DishSummaryResponseDto>>(response.Items);
        PaginatedList<DishSummaryResponseDto> result = new PaginatedList<DishSummaryResponseDto>(mappedDishes, response.CurrentPage, 6, response.Count);

        return result;
    }

    public async Task<DishDetailResponseDto?> GetOneAsync(Guid id)
    {
        var dish = await _unitOfWork.Dishes.GetOneAsync(id);

        if (dish == null)
        {
            throw new NotFoundException($"Dish with ID '{id}' was not found.");
        }

        return _mapper.Map<DishDetailResponseDto>(dish);
    }

    public async Task<DishDetailResponseDto> AddAsync(DishCreateRequestDto request, IFormFile? file)
    {
        var dish = _mapper.Map<Dish>(request);

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

            dish.Picture = await ConvertToBase64(file);
        }

        if (request.AllergenIds != null && request.AllergenIds.Count > 0)
        {
            var allergens = await _unitOfWork.Allergens.FindAsync(request.AllergenIds);
            dish.Allergens.Clear();
            foreach (var allergen in allergens)
            {
                dish.Allergens.Add(allergen);
            }
        }

        await _unitOfWork.Dishes.AddAsync(dish);

        await _unitOfWork.CompleteAsync();

        return _mapper.Map<DishDetailResponseDto>(dish);
    }

    public async Task UpdateAsync(Guid id, DishUpdateRequestDto request, IFormFile? file)
    {
        var dish = await _unitOfWork.Dishes.GetOneAsync(id);

        if (dish == null)
        {
            throw new NotFoundException($"Dish with ID '{id}' was not found.");
        }

        _mapper.Map(request, dish);

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

            dish.Picture = await ConvertToBase64(file);
        }

        if (request.AllergenIds != null && request.AllergenIds.Count > 0)
        {
            var allergens = await _unitOfWork.Allergens.FindAsync(request.AllergenIds);
            dish.Allergens.Clear();
            foreach (var allergen in allergens)
            {
                dish.Allergens.Add(allergen);
            }
        }
        else if (request.AllergenIds == null ||  request.AllergenIds.Count == 0)
        {
            dish.Allergens.Clear();
        }    

            _unitOfWork.Dishes.Update(dish);

        await _unitOfWork.CompleteAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var dish = await _unitOfWork.Dishes.GetOneAsync(id);

        if (dish == null)
        {
            throw new NotFoundException($"Dish with ID '{id}' was not found.");
        }

        _unitOfWork.Dishes.Delete(dish);

        await _unitOfWork.CompleteAsync();
    }

    public async Task<MenuDto?> GetMenuAsync(Guid id)
    {
        var menu = await _unitOfWork.Dishes.GetMenuAsync(id);

        if (menu == null)
        {
            throw new NotFoundException($"Menu with ID '{id}' was not found.");
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
