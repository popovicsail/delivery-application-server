using Delivery.Application.Dtos.AdressValidationDtos.Helpers;
using Delivery.Application.Dtos.AdressValidationDtos.Responses;
using Delivery.Application.Interfaces;
using Newtonsoft.Json;

namespace Delivery.Infrastructure.Services
{
    public class AddressValidationService : IAddressValidationService
    {
        private readonly HttpClient _httpClient;

        public AddressValidationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AddressValidationResultDto> ValidateAsync(string address, string restaurantCity)
        {
            var url = $"https://nominatim.openstreetmap.org/search?q={Uri.EscapeDataString(address)}&format=json&addressdetails=1&limit=5";

            var response = await _httpClient.GetStringAsync(url);
            var results = JsonConvert.DeserializeObject<List<NominatimResult>>(response);

            if (results == null || results.Count == 0)
                return AddressValidationResultDto.Invalid("Adresa nije pronađena.");

            // ✅ Izaberi najbolji rezultat (ako ih ima više)
            var bestMatch = results
            .OrderByDescending(r => CalculateMatchScore(
                new NominatimResult
                {
                    display_name = ToLatin(r.display_name),
                    address = new NominatimAddress
                    {
                        city = ToLatin(r.address?.city),
                        town = ToLatin(r.address?.town),
                        village = ToLatin(r.address?.village),
                        road = ToLatin(r.address?.road),
                        house_number = ToLatin(r.address?.house_number)
                    }
                },
                ToLatin(address)))
            .FirstOrDefault();
            if (bestMatch == null)
                return AddressValidationResultDto.Invalid("Nijedan rezultat se ne poklapa sa adresom.");

            var resultCity = bestMatch.address?.city ?? bestMatch.address?.town ?? bestMatch.address?.village;

            if (string.IsNullOrWhiteSpace(resultCity))
                return AddressValidationResultDto.Invalid("Grad nije moguće odrediti iz adrese.");

            // ✅ Normalizacija pisma (ćirilica → latinica) i poređenje
            var normalizedResultCity = ToLatin(resultCity);
            var normalizedRestaurantCity = ToLatin(restaurantCity);

            if (!normalizedResultCity.Equals(normalizedRestaurantCity, StringComparison.OrdinalIgnoreCase))
                return AddressValidationResultDto.Invalid($"Adresa ne pripada gradu restorana ({restaurantCity}).");

            return AddressValidationResultDto.Valid();
        }

        public async Task<(double Latitude, double Longitude)?> GetCoordinatesAsync(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                return null;

            var url = $"https://nominatim.openstreetmap.org/search?q={Uri.EscapeDataString(address)}&format=json&addressdetails=1&limit=5";

            var response = await _httpClient.GetStringAsync(url);
            var results = JsonConvert.DeserializeObject<List<NominatimResult>>(response);

            if (results == null || results.Count == 0)
                return null;

            // ✅ Izaberi najbolji rezultat koristeći scoring
            var bestMatch = results
                .OrderByDescending(r => CalculateMatchScore(
                    new NominatimResult
                    {
                        display_name = ToLatin(r.display_name),
                        address = new NominatimAddress
                        {
                            city = ToLatin(r.address?.city),
                            town = ToLatin(r.address?.town),
                            village = ToLatin(r.address?.village),
                            road = ToLatin(r.address?.road),
                            house_number = ToLatin(r.address?.house_number)
                        },
                        lat = r.lat,
                        lon = r.lon
                    },
                    ToLatin(address)))
                .FirstOrDefault();

            if (bestMatch == null)
                return null;

            // ✅ Parsiraj lat/lon direktno iz bestMatch
            if (double.TryParse(bestMatch.lat, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var lat) &&
                double.TryParse(bestMatch.lon, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var lon))
            {
                return (lat, lon);
            }

            return null;
        }





        // Helper za poređenje adrese sa rezultatima
        private int CalculateMatchScore(NominatimResult result, string inputAddress)
        {
            int score = 0;

            if (!string.IsNullOrWhiteSpace(result.display_name))
            {
                if (result.display_name.Contains(inputAddress, StringComparison.OrdinalIgnoreCase))
                    score += 100;

                if (!string.IsNullOrWhiteSpace(result.address?.road) &&
                    inputAddress.Contains(result.address.road, StringComparison.OrdinalIgnoreCase))
                    score += 50;

                if (!string.IsNullOrWhiteSpace(result.address?.house_number) &&
                    inputAddress.Contains(result.address.house_number, StringComparison.OrdinalIgnoreCase))
                    score += 20;
            }

            return score;
        }

        private static string ToLatin(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            var map = new (string Cyrillic, string Latin)[]
            {
            // Mala slova
            ("а","a"),("б","b"),("в","v"),("г","g"),("д","d"),("ђ","dj"),
            ("е","e"),("ж","z"),("з","z"),("и","i"),("ј","j"),("к","k"),
            ("л","l"),("љ","lj"),("м","m"),("н","n"),("њ","nj"),("о","o"),
            ("п","p"),("р","r"),("с","s"),("т","t"),("ћ","c"),("у","u"),
            ("ф","f"),("х","h"),("ц","c"),("ч","c"),("џ","dz"),("ш","s"),

            // Velika slova
            ("А","A"),("Б","B"),("В","V"),("Г","G"),("Д","D"),("Ђ","Dj"),
            ("Е","E"),("Ж","Z"),("З","Z"),("И","I"),("Ј","J"),("К","K"),
            ("Л","L"),("Љ","Lj"),("М","M"),("Н","N"),("Њ","Nj"),("О","O"),
            ("П","P"),("Р","R"),("С","S"),("Т","T"),("Ћ","C"),("У","U"),
            ("Ф","F"),("Х","H"),("Ц","C"),("Ч","C"),("Џ","Dz"),("Ш","S")
            };

            var normalized = input.Trim();

            foreach (var (cyr, lat) in map)
            {
                normalized = normalized.Replace(cyr, lat);
            }

            return normalized;
        }


    }
}
