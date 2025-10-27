# Numbeo API .NET Example

A .NET console application that fetches and displays city price data from the Numbeo API.

## Overview

This application demonstrates how to interact with the [Numbeo API](https://www.numbeo.com/common/api.jsp) to retrieve cost of living data for cities around the world. It fetches comprehensive price information including restaurants, groceries, transportation, utilities, and more.

## Features

- Fetch city prices from Numbeo API
- Support for command-line arguments
- Configuration via appsettings.json or environment variables
- Clean, formatted output grouped by category
- Error handling and validation
- Secure API key management

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- A valid Numbeo API key (get one from [Numbeo API page](https://www.numbeo.com/common/api.jsp))

## Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/mladenadamovic/numbeo-api-dotnet.git
cd numbeo-api-dotnet
```

### 2. Configure your API key

You have two options for configuring your Numbeo API key:

#### Option A: Using appsettings.json (recommended for development)

Edit `src/NumbeoPricesApp/appsettings.json`:

```json
{
  "Numbeo": {
    "ApiKey": "your_actual_api_key_here"
  },
  "DefaultCity": "San Francisco, CA",
  "DefaultCountry": "United States"
}
```

#### Option B: Using environment variables (recommended for production)

```bash
export NUMBEO_API_KEY=your_actual_api_key_here
```

On Windows (PowerShell):
```powershell
$env:NUMBEO_API_KEY="your_actual_api_key_here"
```

### 3. Build the application

```bash
dotnet build
```

### 4. Run the application

#### Using default city (San Francisco, CA)

```bash
dotnet run --project src/NumbeoPricesApp
```

#### Specifying a custom city and country

```bash
dotnet run --project src/NumbeoPricesApp "New York, NY" "United States"
```

```bash
dotnet run --project src/NumbeoPricesApp "London" "United Kingdom"
```

```bash
dotnet run --project src/NumbeoPricesApp "Tokyo" "Japan"
```

## Project Structure

```
numbeo-api-dotnet/
├── src/
│   └── NumbeoPricesApp/
│       ├── Models/
│       │   ├── CityPricesResponse.cs    # API response model
│       │   └── PriceItem.cs             # Price item model
│       ├── Services/
│       │   └── NumbeoApiClient.cs       # API client service
│       ├── Program.cs                    # Main application entry point
│       ├── appsettings.json             # Configuration file
│       └── NumbeoPricesApp.csproj       # Project file
├── NumbeoPricesApp.sln                  # Solution file
├── .gitignore
├── LICENSE
└── README.md
```

## Example Output

```
=== Numbeo City Prices App ===

Fetching prices for San Francisco, CA, United States...

================================================================================
City: San Francisco, CA
Country: United States
Currency: USD
Contributors (12 months): 250
Last Update: 10/2024
================================================================================

### Restaurants ###
--------------------------------------------------------------------------------

Meal, Inexpensive Restaurant
  Average Price: 25.00 USD
  Price Range: 15.00 - 35.00 USD
  Data Points: 150

McMeal at McDonalds (or Equivalent Combo Meal)
  Average Price: 12.00 USD
  Price Range: 10.00 - 15.00 USD
  Data Points: 85

...

### Markets ###
--------------------------------------------------------------------------------

Milk (regular), (1 gallon)
  Average Price: 5.50 USD
  Price Range: 4.00 - 7.00 USD
  Data Points: 120

...

================================================================================
Total items: 50
================================================================================
```

## API Response

The Numbeo API returns data in the following structure:

- **City name and country**
- **Currency code**
- **Price items grouped by categories:**
  - Restaurants
  - Markets (Groceries)
  - Transportation
  - Utilities (Monthly)
  - Sports and Leisure
  - Childcare
  - Clothing and Shoes
  - Rent Per Month
  - Buy Apartment Price
  - Salaries And Financing

Each price item includes:
- Item name
- Average price
- Lowest and highest prices
- Number of data points
- Currency code

## Configuration Options

### appsettings.json

```json
{
  "Numbeo": {
    "ApiKey": "your_api_key"
  },
  "DefaultCity": "San Francisco, CA",
  "DefaultCountry": "United States"
}
```

- `Numbeo:ApiKey`: Your Numbeo API key
- `DefaultCity`: Default city to query (used when no command-line arguments provided)
- `DefaultCountry`: Default country to query

## Error Handling

The application includes comprehensive error handling:

- **Missing API key**: Clear error message with instructions
- **Invalid city/country**: HTTP error with helpful debugging information
- **Network errors**: Connection failure messages
- **Invalid JSON**: Parse error handling

## Security

- API keys should never be committed to version control
- `appsettings.Development.json` is included in `.gitignore`
- Use environment variables for production deployments
- The example `appsettings.json` contains placeholder values only

## Development

### Adding new features

The application is structured for easy extension:

1. **Models** (`Models/`): Add new data models for additional API endpoints
2. **Services** (`Services/`): Extend `NumbeoApiClient` with new methods
3. **Program** (`Program.cs`): Add new display logic or command-line options

### Testing with different cities

Some example city/country combinations to try:

```bash
# United States
dotnet run --project src/NumbeoPricesApp "New York, NY" "United States"
dotnet run --project src/NumbeoPricesApp "Los Angeles, CA" "United States"
dotnet run --project src/NumbeoPricesApp "Chicago, IL" "United States"

# Europe
dotnet run --project src/NumbeoPricesApp "London" "United Kingdom"
dotnet run --project src/NumbeoPricesApp "Paris" "France"
dotnet run --project src/NumbeoPricesApp "Berlin" "Germany"

# Asia
dotnet run --project src/NumbeoPricesApp "Tokyo" "Japan"
dotnet run --project src/NumbeoPricesApp "Singapore" "Singapore"
dotnet run --project src/NumbeoPricesApp "Dubai" "United Arab Emirates"
```

## API Documentation

For more information about the Numbeo API, visit:
- [Numbeo API Documentation](https://www.numbeo.com/common/api.jsp)
- [Numbeo API Pricing](https://www.numbeo.com/common/api.jsp)

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## Support

If you encounter any issues or have questions:
1. Check the [Numbeo API documentation](https://www.numbeo.com/common/api.jsp)
2. Verify your API key is valid and active
3. Ensure city and country names match Numbeo's database
4. Open an issue in this repository

## Acknowledgments

- Data provided by [Numbeo](https://www.numbeo.com/) - the world's largest cost of living database
