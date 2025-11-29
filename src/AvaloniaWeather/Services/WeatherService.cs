using AvaloniaWeather.Models;
using AvaloniaWeather.Services.Interfaces;
using AvaloniaWeather.Strings;
using OpenWeatherMapSharp;
using OpenWeatherMapSharp.Models;
using OpenWeatherMapSharp.Models.Enums;

namespace AvaloniaWeather.Services;

/// <summary>
/// Service for fetching weather data from OpenWeatherMap API.
/// </summary>
public class WeatherService : IWeatherService
{
    /// <summary>
    /// API key for OpenWeatherMap API access.
    /// </summary>
    private readonly string _apiKey;

    /// <summary>
    /// Initializes a new instance of the <see cref="WeatherService"/> class.
    /// </summary>
    /// <param name="apiKey">OpenWeatherMap API key</param>
    /// <exception cref="ArgumentException">Thrown if API key is null or empty</exception>
    public WeatherService(string apiKey)
    {
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            throw new ArgumentException("API key cannot be null or empty.", nameof(apiKey));
        }

        _apiKey = apiKey;
    }

    /// <inheritdoc/>
    public async Task<WeatherResult> GetWeatherDataAsync(
        string city,
        LanguageCode language,
        Unit unit)
    {
        try
        {
            // Validate city parameter
            if (string.IsNullOrWhiteSpace(city))
            {
                return new WeatherResult(
                    IsSuccess: false,
                    ErrorMessage: Resources.WeatherServiceExceptionCityNull,
                    CurrentWeather: null,
                    Forecast: null,
                    CurrentAirPollution: null,
                    AirPollutionForecast: null);
            }

            // Initialize the OpenWeatherMap service
            IOpenWeatherMapService weatherService
                = new OpenWeatherMapService(_apiKey);

            // Get geolocation data for the city
            OpenWeatherMapServiceResponse<List<GeocodeInfo>> geolocationResponse
                = await weatherService.GetLocationByNameAsync(city);

            // Handle geolocation response errors
            if (!geolocationResponse.IsSuccess)
            {
                return new WeatherResult(
                    IsSuccess: false,
                    ErrorMessage: Resources.WeatherServiceExceptionGeoLocation,
                    CurrentWeather: null,
                    Forecast: null,
                    CurrentAirPollution: null,
                    AirPollutionForecast: null);
            }

            // Check if any locations were found
            if (geolocationResponse.Response.Count == 0)
            {
                return new WeatherResult(
                    IsSuccess: false,
                    ErrorMessage: Resources.WeatherServiceExceptionCityNotFound,
                    CurrentWeather: null,
                    Forecast: null,
                    CurrentAirPollution: null,
                    AirPollutionForecast: null);
            }

            // Use the first location from the geolocation results
            GeocodeInfo location
                = geolocationResponse.Response.First();

            // Fetch current weather data
            Task<OpenWeatherMapServiceResponse<WeatherRoot>> weatherTask
                = weatherService.GetWeatherAsync(
                    location.Latitude,
                    location.Longitude,
                    language,
                    unit);

            // Fetch weather forecast data
            Task<OpenWeatherMapServiceResponse<ForecastRoot>> forecastTask
                = weatherService.GetForecastAsync(
                    location.Latitude,
                    location.Longitude,
                    language,
                    unit);

            // Fetch current air pollution data
            Task<OpenWeatherMapServiceResponse<AirPollutionRoot>> airPollutionTask
                = weatherService.GetAirPollutionAsync(
                    location.Latitude,
                    location.Longitude);

            // Fetch air pollution forecast data
            Task<OpenWeatherMapServiceResponse<AirPollutionRoot>> airPollutionForecastTask
                = weatherService.GetAirPollutionForecastAsync(
                    location.Latitude,
                    location.Longitude);

            // Await all tasks to complete
            await Task.WhenAll(
                weatherTask,
                forecastTask,
                airPollutionTask,
                airPollutionForecastTask);

            // Retrieve results from completed tasks
            OpenWeatherMapServiceResponse<WeatherRoot> weatherResponse
                = await weatherTask;
            OpenWeatherMapServiceResponse<ForecastRoot> forecastResponse
                = await forecastTask;
            OpenWeatherMapServiceResponse<AirPollutionRoot> airPollutionResponse
                = await airPollutionTask;
            OpenWeatherMapServiceResponse<AirPollutionRoot> airPollutionForecastResponse
                = await airPollutionForecastTask;

            // Check for any errors in the responses
            if (!weatherResponse.IsSuccess)
            {
                return new WeatherResult(
                    IsSuccess: false,
                    ErrorMessage: Resources.WeatherServiceExceptionCurrentWeather,
                    CurrentWeather: null,
                    Forecast: null,
                    CurrentAirPollution: null,
                    AirPollutionForecast: null);
            }

            return new WeatherResult(
                IsSuccess: true,
                ErrorMessage: null,
                CurrentWeather: weatherResponse.Response,
                Forecast: forecastResponse.Response?.Items ?? [],
                CurrentAirPollution: airPollutionResponse.Response?.Entries?.FirstOrDefault(),
                AirPollutionForecast: airPollutionForecastResponse.Response?.Entries ?? []);
        }
        catch (Exception ex)
        {
            return new WeatherResult(
                IsSuccess: false,
                ErrorMessage: ex.Message,
                CurrentWeather: null,
                Forecast: null,
                CurrentAirPollution: null,
                AirPollutionForecast: null);
        }
    }
}
