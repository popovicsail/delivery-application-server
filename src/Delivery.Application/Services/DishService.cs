using AutoMapper;
using Delivery.Application.Dtos.DishDtos.Requests;
using Delivery.Application.Dtos.DishDtos.Responses;
using Delivery.Application.Exceptions;
using Delivery.Application.Interfaces;
using Delivery.Domain.Entities.DishEntities;
using Delivery.Domain.Interfaces;


namespace Delivery.Application.Services;

public class DishService : IDishService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public DishService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<IEnumerable<DishSummaryResponseDto>> GetAllAsync()
    {
        IEnumerable<Dish> dishes = await _unitOfWork.Dishes.GetAllAsync();
        return _mapper.Map<List<DishSummaryResponseDto>>(dishes.ToList());
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

    public async Task<DishDetailResponseDto> AddAsync(DishCreateRequestDto request)
    {
        var dish = _mapper.Map<Dish>(request);

        await _unitOfWork.Dishes.AddAsync(dish);

        await _unitOfWork.CompleteAsync();

        return _mapper.Map<DishDetailResponseDto>(dish);
    }

    public async Task UpdateAsync(Guid id, DishUpdateRequestDto request)
    {
        var dish = await _unitOfWork.Dishes.GetOneAsync(id);

        if (dish == null)
        {
            throw new NotFoundException($"Dish with ID '{id}' was not found.");
        }

        _mapper.Map(request, dish);

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
}
