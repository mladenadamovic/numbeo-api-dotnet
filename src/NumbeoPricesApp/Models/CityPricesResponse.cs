using System.Text.Json.Serialization;

namespace NumbeoPricesApp.Models;

public class CityPricesResponse
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("city")]
    public string City { get; set; } = string.Empty;

    [JsonPropertyName("country")]
    public string Country { get; set; } = string.Empty;

    [JsonPropertyName("currency")]
    public string Currency { get; set; } = string.Empty;

    [JsonPropertyName("prices")]
    public List<PriceItem> Prices { get; set; } = new();

    [JsonPropertyName("contributors_12_months")]
    public int Contributors12Months { get; set; }

    [JsonPropertyName("month_last_update")]
    public int MonthLastUpdate { get; set; }

    [JsonPropertyName("year_last_update")]
    public int YearLastUpdate { get; set; }
}
