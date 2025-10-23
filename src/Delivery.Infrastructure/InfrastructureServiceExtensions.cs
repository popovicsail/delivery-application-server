using Delivery.Domain.Interfaces;
using Delivery.Infrastructure.BackgroundServices.BirthdayVoucherBackgroundJob;
using Delivery.Infrastructure.BackgroundServices.LoggingBackgroundJob;
using Delivery.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Delivery.Infrastructure;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddQuartz();
        services.AddQuartzHostedService(options =>
        {
            options.WaitForJobsToComplete = true;
        });
        services.ConfigureOptions<VoucherExpirationDateCheckerBackgroundJobSetup>();
        services.ConfigureOptions<LoggingBackgroundJobSetup>();

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")).UseSnakeCaseNamingConvention());

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IRestaurantRepository, Repositories.RestaurantRepository>();
        services.AddScoped<IOwnerRepository, Repositories.OwnerRepository>();

        return services;
    }
}

