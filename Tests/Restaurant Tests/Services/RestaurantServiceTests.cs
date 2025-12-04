using Moq;
using Shouldly;
using Delivery.Application.Services;
using Delivery.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Delivery.Domain.Entities.UserEntities;
using Delivery.Domain.Entities.RestaurantEntities;
using Delivery.Application.Exceptions;
using Delivery.Application.Dtos.RestaurantDtos.Responses;
using Delivery.Domain.Entities.CommonEntities;
using Delivery.Application.Dtos.CommonDtos.AddressDtos;
using Delivery.Application.Dtos.Users.OwnerDtos.Responses;
using Delivery.Application.Dtos.CommonDtos.BaseWordSchedDtos;
using Delivery.Application.Dtos.RestaurantDtos;
using Microsoft.AspNetCore.Http;
using Delivery.Infrastructure.Services;
using Microsoft.Extensions.Configuration;

public class RestaurantServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<UserManager<User>> _userManagerMock;
    private readonly Mock<EmailSender> _emailSenderMock;
    private readonly Mock<IConfiguration> _configurationMock;
    public RestaurantServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _userManagerMock = new Mock<UserManager<User>>(
            Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null
        );
    }

    [Fact]
    public async Task GetOneAsync_ShouldThrow_WhenRestaurantNotFound()
    {
        // Arrange
        var restaurantId = Guid.NewGuid();
        var restaurant = new Restaurant { Id = Guid.NewGuid() };
        _unitOfWorkMock.Setup(u => u.Restaurants.GetOneAsync(restaurantId))
            .ReturnsAsync((Restaurant?)null);
        var service = new RestaurantService(_unitOfWorkMock.Object, _mapperMock.Object, _userManagerMock.Object, _emailSenderMock.Object, _configurationMock.Object);

        // Act & Assert
        var ex = await Should.ThrowAsync<NotFoundException>(() => service.GetOneAsync(restaurantId));
        ex.Message.ShouldBe($"Restaurant with ID '{restaurantId}' was not found.");
    }

    [Fact]
    public async Task GetOneAsync_ShouldReturnMappedRestaurant()
    {
        // Arrange
        var restaurantId = Guid.NewGuid();
        var ownerId = Guid.NewGuid();
        var addressId = Guid.NewGuid();
        var baseWorkSchedId = Guid.NewGuid();
        var menuId = Guid.NewGuid();
        var restaurant = new Restaurant { Id = restaurantId, Name = "Pizzeria Roma", Description = "Novi desc", Image = "", PhoneNumber = "00", OwnerId = ownerId, AddressId = addressId };
        restaurant.Address = new Address { Id = addressId, StreetAndNumber = "Main St 1", City = "Cityville", PostalCode = "12345" };
        restaurant.BaseWorkSched = new BaseWorkSched { Id = baseWorkSchedId, RestaurantId = restaurantId, Sunday = true, Saturday = true, WorkDayEnd = new TimeSpan(0), WorkDayStart = new TimeSpan(0) };
        restaurant.Menus.Add(new Menu { Id = menuId, Name = "Main Menu", RestaurantId = restaurantId });
        restaurant.Owner = new Owner { Id = ownerId, UserId = Guid.NewGuid() };
        _unitOfWorkMock.Setup(u => u.Restaurants.GetOneAsync(restaurantId))
            .ReturnsAsync(restaurant);
        var mappedRestaurant = new RestaurantDetailResponseDto {Id = restaurantId, Name = "Pizzeria Roma", Description = "Novi desc", Image = "", PhoneNumber = "00"};
        mappedRestaurant.Address = new AddressDto { StreetAndNumber = "Main St 1", City = "Cityville", PostalCode = "12345" };
        mappedRestaurant.Owner = new OwnerDetailResponseDto { Id = ownerId };
        mappedRestaurant.BaseWorkSched = new BaseWorkSchedDto { Sunday = true, Saturday = true, WorkDayEnd = new TimeSpan(0), WorkDayStart = new TimeSpan(0) };
        mappedRestaurant.Menus = new List<MenuDto>
        {
            new MenuDto { Id = menuId, Name = "Main Menu" }
        };
        _mapperMock.Setup(m => m.Map<RestaurantDetailResponseDto>(restaurant))
            .Returns(mappedRestaurant);
        var service = new RestaurantService(_unitOfWorkMock.Object, _mapperMock.Object, _userManagerMock.Object, _emailSenderMock.Object, _configurationMock.Object);
        // Act
        var result = await service.GetOneAsync(restaurantId);
        // Assert
        result.ShouldBe(mappedRestaurant);
    }

    [Fact]
    public async Task ConverAndAddImageToBase64_ShouldReturnBase64String()
    {
        // Arrange
        var fileClass = new Mock<IFormFile>();
        var file = fileClass.Object;
        using var ms = new MemoryStream();
        await file.CopyToAsync(ms);
        var fileBytes = ms.ToArray();
        
        var expectedBase64 = await RestaurantService.ConvertToBase64(file);
        // Act
        var result = $"data:{file.ContentType};base64,{Convert.ToBase64String(fileBytes)}";
        // Assert
        result.ShouldBe(expectedBase64);
    }

    [Fact]
    public async Task ShouldNotAddImage_WhenFileIsInvalid()
    {
        // Arrange
        IFormFile? file = null;
        var restaurant = new Restaurant();
        const long maxFileSize = 5 * 1024 * 1024; // 5 MB
        var allowedTypes = new[] { "image/png", "image/jpeg" };
        // Act
        if (file != null && file.Length > 0)
        {
            if (file.Length > maxFileSize)
            {
                // Assert
                restaurant.Image.ShouldBeNull();
                return;
            }
            if (!allowedTypes.Contains(file.ContentType))
            {
                // Assert
                restaurant.Image.ShouldBeNull();
                return;
            }

            restaurant.Image = await RestaurantService.ConvertToBase64(file);
        }
        // Assert
        restaurant.Image.ShouldBeNull();
    }
}