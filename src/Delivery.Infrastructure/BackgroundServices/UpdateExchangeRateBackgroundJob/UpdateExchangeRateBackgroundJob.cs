using Delivery.Application.Interfaces;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Delivery.Infrastructure.BackgroundServices.UpdateExchangeRateBackgroundJob
{
    [DisallowConcurrentExecution]
    public class UpdateExchangeRateBackgroundJob : IJob
    {
        private readonly ILogger<UpdateExchangeRateBackgroundJob> _logger;
        private readonly IExchangeRateService _exchangeRateService;

        public UpdateExchangeRateBackgroundJob(ILogger<UpdateExchangeRateBackgroundJob> logger, IExchangeRateService exchangeRateService)
        {
            _logger = logger;
            _exchangeRateService = exchangeRateService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("UpdateExchangeRate started");

            await _exchangeRateService.UpdateExchangeRateAsync();

            _logger.LogInformation("UpdateExchangeRate completed");
        }
    }
}