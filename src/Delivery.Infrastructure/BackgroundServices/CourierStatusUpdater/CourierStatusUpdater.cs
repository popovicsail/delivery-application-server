using Delivery.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Delivery.Infrastructure.BackgroundServices.CourierStatusUpdater;

/// <summary>
/// BackgroundService koji periodično osvežava status svih kurira
/// (AKTIVAN / NEAKTIVAN) na osnovu njihovog radnog vremena.
/// </summary>
public class CourierStatusUpdater : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    // Interval između dva osvežavanja (ovde na 1 minut)
    private readonly TimeSpan _interval = TimeSpan.FromSeconds(5);

    public CourierStatusUpdater(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    /// <summary>
    /// Glavna petlja background servisa.
    /// Dokle god aplikacija radi, ova metoda se izvršava u pozadini.
    /// </summary>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Petlja radi sve dok aplikacija ne bude zaustavljena
        while (!stoppingToken.IsCancellationRequested)
        {
            // Kreiramo novi scope jer HostedService živi ceo vek aplikacije,
            // a servisi koje koristimo (CourierService, UnitOfWork) su scoped.
            using var scope = _scopeFactory.CreateScope();

            // Uzimamo CourierService iz DI kontejnera
            var courierService = scope.ServiceProvider.GetRequiredService<ICourierService>();

            // Pozivamo metodu koja osvežava status svih kurira
            await courierService.UpdateAllCouriersStatusAsync();

            // Pauza do sledećeg ciklusa
            await Task.Delay(_interval, stoppingToken);
        }
    }
}
