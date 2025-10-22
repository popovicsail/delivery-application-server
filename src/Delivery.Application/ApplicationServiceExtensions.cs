using Delivery.Application.Interfaces;
using Delivery.Application.Services;
using Microsoft.Extensions.DependencyInjection;


namespace Delivery.Application;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IOwnerService, OwnerService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAllergenService, AllergenService>();
        services.AddScoped<IWorkerService, WorkerService>();
        services.AddScoped<ICourierService, CourierService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IProfileService, ProfileService>();
        services.AddScoped<IRestaurantService, RestaurantService>();
        services.AddScoped<IDishService, DishService>();
        services.AddScoped<IDishOptionGroupService, DishOptionGroupService>();
        services.AddScoped<IVoucherService, VoucherService>();

        return services;
    }
}