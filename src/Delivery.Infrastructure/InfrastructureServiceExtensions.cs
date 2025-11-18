using Delivery.Domain.Interfaces;
using Delivery.Infrastructure.BackgroundServices.BirthdayVoucherBackgroundJob;
using Delivery.Infrastructure.BackgroundServices.LoggingBackgroundJob;
using Delivery.Infrastructure.Persistence;
using Delivery.Infrastructure.Repositories;
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
        services.AddScoped<IRestaurantRepository, RestaurantRepository>();
        services.AddScoped<IOwnerRepository, OwnerRepository>();
        services.AddScoped<IFeedbackQuestionRepository, FeedbackQuestionRepository>();
        services.AddScoped<IFeedbackResponseRepository, FeedbackResponseRepository>();
        services.AddScoped<IRatingRepository, RatingRepository>();

        return services;
    }
}

