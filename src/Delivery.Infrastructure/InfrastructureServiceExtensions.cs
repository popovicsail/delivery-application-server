using Delivery.Application.Interfaces;
using Delivery.Application.Settings;
using Delivery.Domain.Interfaces;
using Delivery.Infrastructure.BackgroundServices.LoggingBackgroundJob;
using Delivery.Infrastructure.BackgroundServices.UpdateWeatherConditionsBackgroundJob;
using Delivery.Infrastructure.BackgroundServices.VoucherExpirationDateCheckerBackgroundJob;
using Delivery.Infrastructure.Persistence;
using Delivery.Infrastructure.Repositories;
using Delivery.Infrastructure.Services;
using Delivery.Infrastructure.Services.PdfService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Microsoft.Extensions.Logging;
using Quartz;
using QuestPDF.Infrastructure;
using Delivery.Infrastructure.BackgroundServices.UpdateExchangeRateBackgroundJob;
using Delivery.Infrastructure.Persistence.MongoRepositories;

namespace Delivery.Infrastructure;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

        services.AddQuartz();
        services.AddQuartzHostedService(options =>
        {
            options.WaitForJobsToComplete = true;
        });
        services.ConfigureOptions<VoucherExpirationDateCheckerBackgroundJobSetup>();
        services.ConfigureOptions<LoggingBackgroundJobSetup>();
        services.ConfigureOptions<UpdateWeatherConditionsBackgroundJobSetup>();
        services.ConfigureOptions<UpdateExchangeRateBackgroundJobSetup>();

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")).UseSnakeCaseNamingConvention()
            .EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine, LogLevel.Information));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IRestaurantRepository, RestaurantRepository>();
        services.AddScoped<IOwnerRepository, OwnerRepository>();
        services.AddScoped<IFeedbackQuestionRepository, FeedbackQuestionRepository>();
        services.AddScoped<IFeedbackResponseRepository, FeedbackResponseRepository>();
        services.AddScoped<IRatingRepository, RatingRepository>();

        services.AddScoped<IMongoUnitOfWork, MongoUnitOfWork>();

        services.AddScoped<ITokenService, TokenService>();

        services.Configure<EmailSenderSettings>(configuration.GetSection(EmailSenderSettings.SectionName));
        services.AddScoped<IEmailSender, EmailSender>();

        services.AddSingleton<IMongoClient>(sp =>
        {
            var config = configuration.GetSection("MongoDbSettings");
            return new MongoClient(config["ConnectionString"]);
        });

        services.AddScoped<IMongoDatabase>(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            var config = configuration.GetSection("MongoDbSettings");
            return client.GetDatabase(config["DatabaseName"]);
        });
        services.AddScoped<IReportsRepository, ReportsRepository>();

        QuestPDF.Settings.License = LicenseType.Community;

        services.AddScoped<IPdfService, PdfService>();  

        services.Configure<OpenWeatherSettings>(configuration.GetSection(OpenWeatherSettings.SectionName));
        services.AddHttpClient<IOpenWeatherExternalService, OpenWeatherExternalService>();

        services.Configure<ExchangeRateSettings>(configuration.GetSection(ExchangeRateSettings.SectionName));
        services.AddHttpClient<IExchangeRateExternalService, ExchangeRateExternalService>();

        return services;
    }
}

