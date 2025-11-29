namespace AvaloniaWeather.Services.Interfaces;

/// <summary>
/// Factory interface for creating IWeatherService instances.
/// </summary>
public interface IWeatherServiceFactory
{
    /// <summary>
    /// Creates an instance of IWeatherService with the provided API key.
    /// </summary>
    /// <param name="apiKey">OpenWeatherMap API key for authentication</param>
    /// <returns>Configured IWeatherService instance</returns>
    IWeatherService CreateWeatherService(string apiKey);
}
