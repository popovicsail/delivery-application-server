using Delivery.Application.Interfaces;
using Delivery.Application.Services;
using Microsoft.Extensions.Logging;
using Quartz;


namespace Delivery.Infrastructure.BackgroundServices.BirthdayVoucherBackgroundJob;

[DisallowConcurrentExecution]
public class VoucherExpirationDateCheckerBackgroundJob : IJob
{
    private readonly ILogger<VoucherExpirationDateCheckerBackgroundJob> _logger;
    private readonly IVoucherService _voucherService;

    public VoucherExpirationDateCheckerBackgroundJob(ILogger<VoucherExpirationDateCheckerBackgroundJob> logger, IVoucherService voucherService)
    {
        _logger = logger;
        _voucherService = voucherService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("BirthdayVoucherBackgroundJob started: {UtcNow}", DateTime.UtcNow);

        await _voucherService.VoucherExpirationDateCheckerBackgroundJobAsync();

        _logger.LogInformation("BirthdayVoucherBackgroundJob finished: {UtcNow}", DateTime.UtcNow);
    }
}
