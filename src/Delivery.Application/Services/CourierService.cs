using AutoMapper;
using Delivery.Application.Exceptions;
using Delivery.Application.Interfaces;
using Delivery.Domain.Entities.UserEntities;
using Delivery.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Delivery.Application.Dtos.Users.CourierDtos.Requests;
using Delivery.Application.Dtos.Users.CourierDtos.Responses;

namespace Delivery.Application.Services;

public class CourierService : ICourierService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    public CourierService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userManager = userManager;
    }
    public async Task<IEnumerable<CourierSummaryResponseDto>> GetAllAsync()
    {
        IEnumerable<Courier> couriers = await _unitOfWork.Couriers.GetAllAsync();
        return _mapper.Map<List<CourierSummaryResponseDto>>(couriers.ToList());
    }

    public async Task<CourierDetailResponseDto?> GetOneAsync(Guid id)
    {
        var courier = await _unitOfWork.Couriers.GetOneAsync(id);

        if (courier == null)
        {
            throw new NotFoundException($"Courier with ID '{id}' was not found.");
        }

        return _mapper.Map<CourierDetailResponseDto>(courier);
    }

    public async Task<CourierDetailResponseDto> AddAsync(CourierCreateRequestDto request)
    {
        var user = new User
        {
            UserName = request.UserName,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            throw new BadRequestException("ERROR: Error while creating new courier.");
        }

        await _userManager.AddToRoleAsync(user, "Courier");

        var courier = new Courier { UserId = user.Id };
        await _unitOfWork.Couriers.AddAsync(courier);
        await _unitOfWork.CompleteAsync();

        var createdCourier = await _unitOfWork.Couriers.GetOneAsync(courier.Id);

        return _mapper.Map<CourierDetailResponseDto>(createdCourier);
    }

    public async Task UpdateAsync(Guid id, CourierUpdateRequestDto courierDto)
    {
        var courier = await _unitOfWork.Couriers.GetOneAsync(id);

        if (courier == null)
        {
            throw new NotFoundException($"Courier with ID '{id}' was not found.");
        }

        _mapper.Map(courierDto, courier);

        _unitOfWork.Couriers.Update(courier);

        await _unitOfWork.CompleteAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var courier = await _unitOfWork.Couriers.GetOneAsync(id);

        if (courier == null)
        {
            throw new NotFoundException($"Courier with ID '{id}' was not found.");
        }

        _unitOfWork.Couriers.Update(courier);

        await _unitOfWork.CompleteAsync();
    }
}
