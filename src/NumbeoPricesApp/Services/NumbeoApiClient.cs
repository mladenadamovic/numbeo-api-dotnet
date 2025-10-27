using System.Text.Json;
using System.Web;
using NumbeoPricesApp.Models;

namespace NumbeoPricesApp.Services;

public class NumbeoApiClient : IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private const string BaseUrl = "https://www.numbeo.com/api";

    public NumbeoApiClient(string apiKey)
    {
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            throw new ArgumentException("API key cannot be null or empty", nameof(apiKey));
        }

        _apiKey = apiKey;
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(BaseUrl)
        };
    }

    /// <summary>
    /// Fetches city prices from the Numbeo API
    /// </summary>
    /// <param name="city">City name (e.g., "San Francisco, CA")</param>
    /// <param name="country">Country name (e.g., "United States")</param>
    /// <returns>City prices response</returns>
    public async Task<CityPricesResponse?> GetCityPricesAsync(string city, string country)
    {
        if (string.IsNullOrWhiteSpace(city))
        {
            throw new ArgumentException("City cannot be null or empty", nameof(city));
        }

        if (string.IsNullOrWhiteSpace(country))
        {
            throw new ArgumentException("Country cannot be null or empty", nameof(country));
        }

        try
        {
            // Build the query string with proper URL encoding
            var queryParams = HttpUtility.ParseQueryString(string.Empty);
            queryParams["city"] = city;
            queryParams["country"] = country;
            queryParams["api_key"] = _apiKey;

            var url = $"/city_prices?{queryParams}";

            Console.WriteLine($"Fetching prices for {city}, {country}...");

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException(
                    $"API request failed with status code {response.StatusCode}. Response: {errorContent}");
            }

            var content = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<CityPricesResponse>(content, options);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error fetching city prices: {ex.Message}");
            throw;
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Error parsing API response: {ex.Message}");
            throw;
        }
    }

    public void Dispose()
    {
        _httpClient?.Dispose();
    }
}
