using System.Globalization;
using Delivery.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

public class DeliveryTimeService : IDeliveryTimeService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public DeliveryTimeService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _apiKey = config["OpenRouteService:ApiKey"];
    }

    /// <summary>
    /// Računa procenjeno vreme dostave u minutima koristeći OpenRouteService API.
    /// </summary>
    public async Task<int?> GetEstimatedDeliveryTimeMinutesAsync(double restaurantLat, double restaurantLon, double customerLat, double customerLon)
    {
        var url = $"https://api.openrouteservice.org/v2/directions/driving-car" +
           $"?api_key={_apiKey}" +
           $"&start={restaurantLon.ToString(CultureInfo.InvariantCulture)},{restaurantLat.ToString(CultureInfo.InvariantCulture)}" +
           $"&end={customerLon.ToString(CultureInfo.InvariantCulture)},{customerLat.ToString(CultureInfo.InvariantCulture)}";

        var response = await _httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode)
            return null;

        var json = await response.Content.ReadAsStringAsync();
        var obj = JObject.Parse(json);

        // duration je u sekundama
        var durationSeconds = (int?)obj["features"]?[0]?["properties"]?["summary"]?["duration"];
        if (durationSeconds == null)
            return null;

        return durationSeconds.Value / 60; // pretvori u minute
    }
}
