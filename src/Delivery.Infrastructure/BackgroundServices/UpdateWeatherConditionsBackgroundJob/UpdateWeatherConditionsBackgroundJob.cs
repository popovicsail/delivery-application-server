using Delivery.Application.Interfaces;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Delivery.Infrastructure.BackgroundServices.UpdateWeatherConditionsBackgroundJob;

[DisallowConcurrentExecution]
public class UpdateWeatherConditionsBackgroundJob : IJob
{
    private readonly ILogger<UpdateWeatherConditionsBackgroundJob> _logger;
    private readonly IWeatherService _openWeatherService;

    public UpdateWeatherConditionsBackgroundJob(ILogger<UpdateWeatherConditionsBackgroundJob> logger, IWeatherService openWeatherService)
    {
        _logger = logger;
        _openWeatherService = openWeatherService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("UpdateWeatherTask started");

        await _openWeatherService.UpdateWeatherConditionsAsync();

        _logger.LogInformation("UpdateWeatherTask completed");
    }
}