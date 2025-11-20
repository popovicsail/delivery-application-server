using Moq;
using Shouldly;
using Delivery.Application.Services;
using Delivery.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Delivery.Domain.Entities.UserEntities;
using Delivery.Application.Exceptions;

public class WorkerServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<UserManager<User>> _userManagerMock;
    public WorkerServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _userManagerMock = new Mock<UserManager<User>>(
            Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null
        );
    }

    [Fact]
    public async Task SuspendWorkerAsync_ShouldSuspendWorker_WhenWorkerExists()
    {
        // Arrange
        var workerId = Guid.NewGuid();
        var worker = new Worker { Id = workerId, IsSuspended = false };
        var newWorker = new Mock<Worker>();
        _unitOfWorkMock.Setup(u => u.Workers.GetOneAsync(workerId))
            .ReturnsAsync(worker);
        _unitOfWorkMock.Setup(u => u.Workers.Update(worker));
        _unitOfWorkMock.Setup(u => u.CompleteAsync())
            .ReturnsAsync(1);
        var workerService = new WorkerService(
            _unitOfWorkMock.Object,
            _mapperMock.Object,
            _userManagerMock.Object
        );
        // Act
        await workerService.SuspendWorkerAsync(workerId);
        // Assert
        worker.IsSuspended.ShouldBeTrue();
        _unitOfWorkMock.Verify(u => u.Workers.Update(worker), Times.Once);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task SuspendWorkerAsync_ShouldThrowNotFoundException_WhenWorkerDoesNotExist()
    {
        // Arrange
        var workerId = Guid.NewGuid();
        _unitOfWorkMock.Setup(u => u.Workers.GetOneAsync(workerId))
            .ReturnsAsync((Worker?)null);
        var workerService = new WorkerService(
            _unitOfWorkMock.Object,
            _mapperMock.Object,
            _userManagerMock.Object
        );
        // Act & Assert
        await Should.ThrowAsync<NotFoundException>(async () =>
        {
            await workerService.SuspendWorkerAsync(workerId);
        });
    }
}