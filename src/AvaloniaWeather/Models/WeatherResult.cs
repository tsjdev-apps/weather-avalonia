using OpenWeatherMapSharp.Models;

namespace AvaloniaWeather.Models;

/// <summary>
/// Represents the result of a weather data retrieval operation.
/// </summary>
/// <param name="IsSuccess">True if data was retrieved successfully</param>
/// <param name="ErrorMessage">Error description if IsSuccess is false</param>
/// <param name="CurrentWeather">Current weather conditions</param>
/// <param name="Forecast">5-day forecast (3-hour intervals)</param>
/// <param name="CurrentAirPollution">Current air quality data</param>
/// <param name="AirPollutionForecast">Forecasted air quality data</param>
public record WeatherResult(
    bool IsSuccess,
    string? ErrorMessage,
    WeatherRoot? CurrentWeather,
    IReadOnlyList<ForecastItem>? Forecast,
    AirPollutionEntry? CurrentAirPollution,
    IReadOnlyList<AirPollutionEntry>? AirPollutionForecast);
