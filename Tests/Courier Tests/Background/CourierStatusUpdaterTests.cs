using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Delivery.Application.BackgroundServices;
using Delivery.Application.Interfaces;
using Delivery.Application.Services;
using Delivery.Domain.Entities.CommonEntities;
using Delivery.Domain.Entities.UserEntities;
using Delivery.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Shouldly;
using Xunit;

public class CourierStatusUpdaterTests
{
    [Fact]
    public async Task UpdateAllCouriersStatusAsync_ShouldSetActive_WhenNowWithinSchedule()
    {
        // Arrange
        var now = DateTime.Now;
        var courier = new Courier
        {
            WorkSchedules = new List<WorkSchedule>
            {
                new WorkSchedule
                {
                    WeekDay = now.DayOfWeek.ToString(),
                    WorkStart = now.TimeOfDay.Add(TimeSpan.FromMinutes(-30)),
                    WorkEnd = now.TimeOfDay.Add(TimeSpan.FromMinutes(30))
                }
            },
            WorkStatus = "NEAKTIVAN"
        };

        var couriers = new List<Courier> { courier };

        var courierRepoMock = new Mock<ICourierRepository>();
        courierRepoMock.Setup(r => r.GetAllAsync())
                       .ReturnsAsync(couriers);

        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock.Setup(u => u.Couriers).Returns(courierRepoMock.Object);

        var mapperMock = new Mock<IMapper>();
        var userManagerMock = new Mock<UserManager<User>>(
            Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);

        var service = new CourierService(unitOfWorkMock.Object, mapperMock.Object, userManagerMock.Object);

        // Act
        await service.UpdateAllCouriersStatusAsync();

        // Assert
        courier.WorkStatus.ShouldBe("AKTIVAN");
    }
}
