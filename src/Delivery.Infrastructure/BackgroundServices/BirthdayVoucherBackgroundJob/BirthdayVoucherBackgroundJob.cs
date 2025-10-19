using Delivery.Application.Interfaces;
using Microsoft.Extensions.Logging;
using Quartz;


namespace Delivery.Infrastructure.BackgroundServices.BirthdayVoucherBackgroundJob;

[DisallowConcurrentExecution]
public class BirthdayVoucherBackgroundJob : IJob
{
    private readonly ILogger<BirthdayVoucherBackgroundJob> _logger;
    private readonly ICustomerService _customerService;

    public BirthdayVoucherBackgroundJob(ILogger<BirthdayVoucherBackgroundJob> logger, ICustomerService customerService)
    {
        _logger = logger;
        _customerService = customerService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("BirthdayVoucherBackgroundJob started: {UtcNow}", DateTime.UtcNow);

        await _customerService.BirthdayVoucherBackgroundJobAsync();

        _logger.LogInformation("BirthdayVoucherBackgroundJob finished: {UtcNow}", DateTime.UtcNow);
    }
}
