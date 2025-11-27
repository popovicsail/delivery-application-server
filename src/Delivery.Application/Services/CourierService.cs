using AutoMapper;
using Delivery.Application.Dtos.CommonDtos.WorkScheduleDto;
using Delivery.Application.Dtos.Users.CourierDtos.Requests;
using Delivery.Application.Dtos.Users.CourierDtos.Responses;
using Delivery.Application.Exceptions;
using Delivery.Application.Interfaces;
using Delivery.Domain.Entities.CommonEntities;
using Delivery.Domain.Entities.UserEntities;
using Delivery.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

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
        var courier = await _unitOfWork.Couriers.GetOneWithUserAsync(id);

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

    public async Task UpdateWorkSchedulesAsync(Guid courierId, CourierWorkSchedulesUpdateRequestDto request)
    {
        var courier = await _unitOfWork.Couriers.GetOneWithUserAsync(courierId);
        if (courier == null)
            throw new NotFoundException($"Courier with ID '{courierId}' was not found.");

        // ✅ Validacija dnevnog limita (≤10h)
        foreach (var s in request.Schedules)
        {
            var dailyHours = (s.WorkEnd - s.WorkStart).TotalHours;
            if (dailyHours > 10)
                throw new BadRequestException("Radno vreme ne može biti duže od 10h dnevno.");
        }

        // ✅ Validacija nedeljnog limita (≤40h)
        var totalHours = request.Schedules.Sum(s => (s.WorkEnd - s.WorkStart).TotalHours);
        if (totalHours > 40)
            throw new BadRequestException("Ukupno radno vreme ne može biti duže od 40h nedeljno.");

        // Očisti stare rasporede
        courier.WorkSchedules.Clear();

        // Mapiraj nove rasporede pomoću AutoMapper-a
        var newSchedules = _mapper.Map<List<WorkSchedule>>(request.Schedules);
        foreach (var schedule in newSchedules)
        {
            courier.WorkSchedules.Add(schedule);
        }

        _unitOfWork.Couriers.Update(courier);
        await _unitOfWork.CompleteAsync();
    }


    public async Task UpdateAllCouriersStatusAsync()
    {
        var couriers = await _unitOfWork.Couriers.GetAllWithSchedulesAsync();
        var now = DateTime.UtcNow;
        var today = now.DayOfWeek.ToString();
        var yesterday = now.AddDays(-1).DayOfWeek.ToString();

        foreach (var courier in couriers)
        {
            var isActive = courier.WorkSchedules
                .Where(ws =>
                    ws.WeekDay.Equals(today, StringComparison.OrdinalIgnoreCase) ||
                    ws.WeekDay.Equals(yesterday, StringComparison.OrdinalIgnoreCase)
                )
                .Any(ws => InShift(now.TimeOfDay, ws.WorkStart, ws.WorkEnd));

            courier.WorkStatus = isActive ? "AKTIVAN" : "NEAKTIVAN";
            _unitOfWork.Couriers.Update(courier);
        }

        await _unitOfWork.CompleteAsync();
    }

    public async Task<CourierStatusResponseDto> GetCourierStatusAsync(Guid courierId)
    {
        var courier = await _unitOfWork.Couriers.GetOneWithUserAsync(courierId);
        if (courier == null)
            throw new NotFoundException($"Courier with ID '{courierId}' was not found.");

        return new CourierStatusResponseDto
        {
            Status = courier.WorkStatus
        };
    }

    public async Task<IEnumerable<WorkScheduleDto>> GetMyWorkSchedulesAsync(Guid courierId)
    {
        var courier = await _unitOfWork.Couriers.GetOneWithUserAsync(courierId);

        if (courier == null)
            throw new NotFoundException("Kurir nije pronađen.");

        return _mapper.Map<IEnumerable<WorkScheduleDto>>(courier.WorkSchedules);
    }



    private bool InShift(TimeSpan now, TimeSpan start, TimeSpan end)
    {
        if (start <= end)
        {
            // normalna smena u istom danu
            return now >= start && now <= end;
        }
        else
        {
            // smena prelazi preko ponoći (npr. 22:00–02:00)
            return now >= start || now <= end;
        }
    }
}
