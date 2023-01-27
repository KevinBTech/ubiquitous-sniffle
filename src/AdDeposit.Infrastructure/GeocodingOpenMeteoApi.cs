using AdDeposit.Domain.Entities;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace AdDeposit.Infrastructure
{
    internal sealed class GeocodingOpenMeteoApi
    {
        private readonly HttpClient _httpClient;

        public GeocodingOpenMeteoApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://geocoding-api.open-meteo.com/");
        }

        public async Task<GeographicCoordinate?> GetAsync(string city, string country)
        {
            var response = await _httpClient.GetFromJsonAsync<GeocodingOpenMeteoResponse>($"v1/search?name={city}&country={country}&count=1");

            var option = response?.Results.FirstOrDefault();

            if (option != null)
            {
                return new GeographicCoordinate(option.Latitude, option.Longitude);
            }

            return null;
        }
    }

    public class Result
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }
    }

    public class GeocodingOpenMeteoResponse
    {
        [JsonPropertyName("results")]
        public List<Result> Results { get; set; }
    }
}