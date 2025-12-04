using Microsoft.Extensions.Options;
using Quartz;

namespace Delivery.Infrastructure.BackgroundServices.UpdateExchangeRateBackgroundJob
{
    public class UpdateExchangeRateBackgroundJobSetup : IConfigureOptions<QuartzOptions>
    {
        public void Configure(QuartzOptions options)
        {
            JobKey jobKey = JobKey.Create(nameof(UpdateExchangeRateBackgroundJob));
            options
                .AddJob<UpdateExchangeRateBackgroundJob>(jobBuilder => jobBuilder.WithIdentity(jobKey))
                .AddTrigger(trigger =>
                    trigger
                        .ForJob(jobKey)
                        .WithCronSchedule("0 */1 * * * ?"));
        }
    }
}