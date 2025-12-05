using Microsoft.Extensions.Options;
using Quartz;

namespace Delivery.Infrastructure.BackgroundServices.UpdateWeatherConditionsBackgroundJob;

public class UpdateWeatherConditionsBackgroundJobSetup : IConfigureOptions<QuartzOptions>
{
    public void Configure(QuartzOptions options)
    {
        JobKey jobKey = JobKey.Create(nameof(UpdateWeatherConditionsBackgroundJob));
        options
            .AddJob<UpdateWeatherConditionsBackgroundJob>(jobBuilder => jobBuilder.WithIdentity(jobKey))
            .AddTrigger(trigger =>
                trigger
                    .ForJob(jobKey)
                    .WithCronSchedule("0 */2 * * * ?"));
    }
}