using Microsoft.Extensions.Options;
using Quartz;

namespace Delivery.Infrastructure.BackgroundServices.BirthdayVoucherBackgroundJob
{
    public class VoucherExpirationDateCheckerBackgroundJobSetup : IConfigureOptions<QuartzOptions>
    {
        public void Configure(QuartzOptions options)
        {
            JobKey jobKey = JobKey.Create(nameof(VoucherExpirationDateCheckerBackgroundJob));
            options
                .AddJob<VoucherExpirationDateCheckerBackgroundJob>(jobBuilder => jobBuilder.WithIdentity(jobKey))
                .AddTrigger(trigger =>
                    trigger
                        .ForJob(jobKey)
                        .WithCronSchedule("0 0 0 * * ?"));
        }
    }
}
