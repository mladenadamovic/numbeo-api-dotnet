using System.Text.Json.Serialization;

namespace NumbeoPricesApp.Models;

public class PriceItem
{
    [JsonPropertyName("item_id")]
    public int ItemId { get; set; }

    [JsonPropertyName("item_name")]
    public string ItemName { get; set; } = string.Empty;

    [JsonPropertyName("category")]
    public string Category { get; set; } = string.Empty;

    [JsonPropertyName("average_price")]
    public decimal AveragePrice { get; set; }

    [JsonPropertyName("lowest_price")]
    public decimal LowestPrice { get; set; }

    [JsonPropertyName("highest_price")]
    public decimal HighestPrice { get; set; }

    [JsonPropertyName("data_points")]
    public int DataPoints { get; set; }

    [JsonPropertyName("currency_code")]
    public string CurrencyCode { get; set; } = string.Empty;
}
