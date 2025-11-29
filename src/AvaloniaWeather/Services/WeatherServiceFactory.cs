using AvaloniaWeather.Services.Interfaces;

namespace AvaloniaWeather.Services;

/// <summary>
/// Factory for creating IWeatherService instances.
/// </summary>
public class WeatherServiceFactory : IWeatherServiceFactory
{
    /// <inheritdoc/>
    public IWeatherService CreateWeatherService(string apiKey)
        => new WeatherService(apiKey);
}
