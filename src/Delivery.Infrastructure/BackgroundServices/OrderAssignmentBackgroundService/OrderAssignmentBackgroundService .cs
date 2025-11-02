using System;
using System.Threading;
using System.Threading.Tasks;
using Delivery.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class OrderAssignmentBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public OrderAssignmentBackgroundService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();

                try
                {
                    await orderService.AutoAssignOrdersAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Greška u auto-assign jobu: {ex.Message}");
                }
            }

            // čekaj 5 sec pre sledeće provere
            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }
}
