using Microsoft.Extensions.Configuration;
using NumbeoPricesApp.Services;

namespace NumbeoPricesApp;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("=== Numbeo City Prices App ===\n");

        // Build configuration
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        // Get API key from configuration
        var apiKey = configuration["Numbeo:ApiKey"] ?? Environment.GetEnvironmentVariable("NUMBEO_API_KEY");

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            Console.WriteLine("ERROR: Numbeo API key is not configured.");
            Console.WriteLine("Please set the API key in one of the following ways:");
            Console.WriteLine("1. Add it to appsettings.json under 'Numbeo:ApiKey'");
            Console.WriteLine("2. Set the NUMBEO_API_KEY environment variable");
            Console.WriteLine("\nExample: export NUMBEO_API_KEY=your_api_key_here");
            return;
        }

        // Get city and country from command line arguments or use defaults
        string city;
        string country;

        if (args.Length >= 2)
        {
            city = args[0];
            country = args[1];
        }
        else
        {
            // Use default values or prompt user
            city = configuration["DefaultCity"] ?? "San Francisco, CA";
            country = configuration["DefaultCountry"] ?? "United States";

            Console.WriteLine($"Using default location: {city}, {country}");
            Console.WriteLine("(You can specify a different city and country as command line arguments)\n");
        }

        try
        {
            using var client = new NumbeoApiClient(apiKey);
            var response = await client.GetCityPricesAsync(city, country);

            if (response == null)
            {
                Console.WriteLine("No data received from the API.");
                return;
            }

            // Display the results
            DisplayResults(response);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"\nError: Failed to fetch data from Numbeo API.");
            Console.WriteLine($"Details: {ex.Message}");
            Console.WriteLine("\nPlease check:");
            Console.WriteLine("- Your API key is valid");
            Console.WriteLine("- The city and country names are correct");
            Console.WriteLine("- You have an active internet connection");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nUnexpected error: {ex.Message}");
        }
    }

    static void DisplayResults(NumbeoPricesApp.Models.CityPricesResponse response)
    {
        Console.WriteLine("\n" + new string('=', 80));
        Console.WriteLine($"City: {response.City}");
        Console.WriteLine($"Country: {response.Country}");
        Console.WriteLine($"Currency: {response.Currency}");
        Console.WriteLine($"Contributors (12 months): {response.Contributors12Months}");
        Console.WriteLine($"Last Update: {response.MonthLastUpdate}/{response.YearLastUpdate}");
        Console.WriteLine(new string('=', 80) + "\n");

        if (response.Prices == null || !response.Prices.Any())
        {
            Console.WriteLine("No price data available for this location.");
            return;
        }

        // Group prices by category
        var groupedPrices = response.Prices.GroupBy(p => p.Category)
            .OrderBy(g => g.Key);

        foreach (var group in groupedPrices)
        {
            Console.WriteLine($"\n### {group.Key} ###");
            Console.WriteLine(new string('-', 80));

            foreach (var item in group.OrderBy(i => i.ItemName))
            {
                Console.WriteLine($"\n{item.ItemName}");
                Console.WriteLine($"  Average Price: {item.AveragePrice:F2} {item.CurrencyCode}");

                if (item.LowestPrice > 0 && item.HighestPrice > 0)
                {
                    Console.WriteLine($"  Price Range: {item.LowestPrice:F2} - {item.HighestPrice:F2} {item.CurrencyCode}");
                }

                if (item.DataPoints > 0)
                {
                    Console.WriteLine($"  Data Points: {item.DataPoints}");
                }
            }
        }

        Console.WriteLine("\n" + new string('=', 80));
        Console.WriteLine($"Total items: {response.Prices.Count}");
        Console.WriteLine(new string('=', 80));
    }
}
