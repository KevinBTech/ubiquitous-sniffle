using AdDeposit.Domain.Ads.Wheather;
using AdDeposit.Domain.Entities;
using System.Globalization;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace AdDeposit.Infrastructure
{
    internal sealed class WheaterForecastOpenMeteoApi
    {
        private readonly HttpClient _httpClient;

        public WheaterForecastOpenMeteoApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://api.open-meteo.com/");
        }

        public async Task<AdWheater?> GetAsync(GeographicCoordinate geographicCoordinate)
        {
            var query = $"v1/forecast?" +
                $"latitude={geographicCoordinate.Latitude.ToString(CultureInfo.InvariantCulture)}" +
                $"&longitude={geographicCoordinate.Longitude.ToString(CultureInfo.InvariantCulture)}" +
                $"&current_weather=true&hourly=temperature_2m";

            var result = await _httpClient.GetFromJsonAsync<OpenMeteoResponse>(query);

            if (result == null)
            {
                return null;
            }

            return new AdWheater(result.CurrentWeather.Time, result.CurrentWeather.Temperature);
        }
    }

    public class CurrentWeather
    {
        [JsonPropertyName("time")]
        public DateTime Time { get; set; }

        [JsonPropertyName("temperature")]
        public double Temperature { get; set; }
    }

    public class OpenMeteoResponse
    {
        [JsonPropertyName("current_weather")]
        public CurrentWeather CurrentWeather { get; set; }
    }
}