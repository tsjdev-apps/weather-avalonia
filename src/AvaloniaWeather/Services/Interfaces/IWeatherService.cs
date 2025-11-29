using AvaloniaWeather.Models;
using OpenWeatherMapSharp.Models.Enums;

namespace AvaloniaWeather.Services.Interfaces;

/// <summary>
/// Service interface for fetching weather data from OpenWeatherMap API.
/// </summary>
public interface IWeatherService
{
    /// <summary>
    /// Fetches weather data for the specified city.
    /// </summary>
    /// <param name="city">City name (e.g., "London", "New York")</param>
    /// <param name="language">Language for weather descriptions</param>
    /// <param name="unit">Unit system (Metric, Imperial, Standard)</param>
    /// <returns>
    /// WeatherResult containing all weather data or error information
    /// </returns>
    Task<WeatherResult> GetWeatherDataAsync(
        string city,
        LanguageCode language,
        Unit unit);
}
