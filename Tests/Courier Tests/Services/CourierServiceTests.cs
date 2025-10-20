using Xunit;
using Moq;
using Shouldly;
using Delivery.Application.Services;
using Delivery.Application.Dtos.Users.CourierDtos.Requests;
using Delivery.Application.Exceptions;
using Delivery.Domain.Entities.UserEntities;
using Delivery.Domain.Entities.CommonEntities;
using Delivery.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

public class CourierServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<UserManager<User>> _userManagerMock;

    public CourierServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _userManagerMock = new Mock<UserManager<User>>(
            Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null
        );
    }

    [Fact]
    public async Task UpdateWorkSchedulesAsync_ShouldThrow_WhenDailyHoursExceed10()
    {
        // Arrange
        var courierId = Guid.NewGuid();
        var request = new CourierWorkSchedulesUpdateRequestDto
        {
            Schedules = new List<WorkScheduleUpdateRequestDto>
            {
                new WorkScheduleUpdateRequestDto
                {
                    WeekDay = "Monday",
                    WorkStart = new TimeSpan(8, 0, 0),
                    WorkEnd = new TimeSpan(20, 30, 0) // 12.5h
                }
            }
        };

        var courier = new Courier { Id = courierId };
        _unitOfWorkMock.Setup(u => u.Couriers.GetOneWithUserAsync(courierId))
            .ReturnsAsync(courier);

        var service = new CourierService(_unitOfWorkMock.Object, _mapperMock.Object, _userManagerMock.Object);

        // Act & Assert
        var ex = await Should.ThrowAsync<BadRequestException>(() => service.UpdateWorkSchedulesAsync(courierId, request));
        ex.Message.ShouldBe("Radno vreme ne može biti duže od 10h dnevno.");
    }

    [Fact]
    public async Task UpdateWorkSchedulesAsync_ShouldThrow_WhenWeeklyHoursExceed40()
    {
        var courierId = Guid.NewGuid();
        var request = new CourierWorkSchedulesUpdateRequestDto
        {
            Schedules = Enumerable.Range(1, 5).Select(i =>
                new WorkScheduleUpdateRequestDto
                {
                    WeekDay = ((DayOfWeek)i).ToString(),
                    WorkStart = new TimeSpan(8, 0, 0),
                    WorkEnd = new TimeSpan(17, 0, 0) // 9h * 5 = 45h
                }).ToList()
        };

        var courier = new Courier { Id = courierId, WorkSchedules = new List<WorkSchedule>() };
        _unitOfWorkMock.Setup(u => u.Couriers.GetOneWithUserAsync(courierId)).ReturnsAsync(courier);

        var service = new CourierService(_unitOfWorkMock.Object, _mapperMock.Object, _userManagerMock.Object);

        var ex = await Should.ThrowAsync<BadRequestException>(() => service.UpdateWorkSchedulesAsync(courierId, request));
        ex.Message.ShouldBe("Ukupno radno vreme ne može biti duže od 40h nedeljno.");
    }

    [Fact]
    public async Task UpdateAllCouriersStatusAsync_ShouldSetStatusToActive_WhenNowWithinSchedule()
    {
        // Arrange
        var now = DateTime.Now;
        var courier = new Courier
        {
            Id = Guid.NewGuid(),
            WorkSchedules = new List<WorkSchedule>
            {
                new WorkSchedule
                {
                    WeekDay = now.DayOfWeek.ToString(),
                    WorkStart = now.TimeOfDay.Subtract(TimeSpan.FromHours(1)),
                    WorkEnd = now.TimeOfDay.Add(TimeSpan.FromHours(1))
                }
            }
        };

        _unitOfWorkMock.Setup(u => u.Couriers.GetAllAsync())
            .ReturnsAsync(new List<Courier> { courier });

        var service = new CourierService(_unitOfWorkMock.Object, _mapperMock.Object, _userManagerMock.Object);

        // Act
        await service.UpdateAllCouriersStatusAsync();

        // Assert
        courier.WorkStatus.ShouldBe("AKTIVAN");
    }

    [Fact]
    public async Task UpdateAllCouriersStatusAsync_ShouldSetStatusToInactive_WhenNowOutsideSchedule()
    {
        // Arrange
        var now = DateTime.UtcNow;
        var courier = new Courier
        {
            Id = Guid.NewGuid(),
            WorkSchedules = new List<WorkSchedule>
            {
                new WorkSchedule
                {
                    WeekDay = now.DayOfWeek.ToString(),
                    WorkStart = now.TimeOfDay.Subtract(TimeSpan.FromHours(3)),
                    WorkEnd = now.TimeOfDay.Subtract(TimeSpan.FromHours(2))
                }
            }
        };

        _unitOfWorkMock.Setup(u => u.Couriers.GetAllAsync())
            .ReturnsAsync(new List<Courier> { courier });

        var service = new CourierService(_unitOfWorkMock.Object, _mapperMock.Object, _userManagerMock.Object);

        // Act
        await service.UpdateAllCouriersStatusAsync();

        // Assert
        courier.WorkStatus.ShouldBe("NEAKTIVAN");
    }
}
