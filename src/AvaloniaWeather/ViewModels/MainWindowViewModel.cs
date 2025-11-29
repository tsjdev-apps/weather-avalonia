using System.Collections.ObjectModel;
using AvaloniaWeather.Models;
using AvaloniaWeather.Services.Interfaces;
using AvaloniaWeather.Strings;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OpenWeatherMapSharp.Models;
using OpenWeatherMapSharp.Models.Enums;

namespace AvaloniaWeather.ViewModels;

public partial class MainWindowViewModel(
    IWeatherServiceFactory weatherServiceFactory) : ViewModelBase
{
    /// <summary>
    /// API key for accessing the weather service.
    /// </summary>
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(GetWeatherCommand))]
    private string _apiKey = string.Empty;

    /// <summary>
    /// City for which to fetch the weather data.
    /// </summary>
    [ObservableProperty]
    private string _city = "Pforzheim";

    /// <summary>
    /// Holds the current weather data.
    /// </summary>
    [ObservableProperty]
    private WeatherRoot? _weatherRoot = null;

    /// <summary>
    /// Holds the forecast items.
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<ForecastItem> _forecastItems = [];

    /// <summary>
    /// Holds the current air pollution data.
    /// </summary>
    [ObservableProperty]
    private AirPollutionEntry? _currentAirPollution = null;

    /// <summary>
    /// Holds the air pollution forecast data.
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<AirPollutionEntry> _airPollutionForecast = [];

    /// <summary>
    /// Available languages for weather data.
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<LanguageCode> _languages = [.. Enum.GetValues<LanguageCode>()];

    /// <summary>
    /// Selected language for weather data.
    /// </summary>
    [ObservableProperty]
    private LanguageCode _selectedLanguage = LanguageCode.EN;

    /// <summary>
    /// Available units for weather data.
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<Unit> _units = new(Enum.GetValues<Unit>());

    /// <summary>
    /// Selected unit for weather data.
    /// </summary>
    [ObservableProperty]
    private Unit _selectedUnit = Unit.Standard;

    /// <summary>
    /// Called when SelectedLanguage changes.
    /// </summary>
    /// <param name="value"></param>
    partial void OnSelectedLanguageChanged(LanguageCode value)
    {
        WeatherRoot = null;
        ForecastItems.Clear();
        ClearError();
    }

    /// <summary>
    /// Called when SelectedUnit changes.
    /// </summary>
    /// <param name="value"></param>
    partial void OnSelectedUnitChanged(Unit value)
    {
        WeatherRoot = null;
        ForecastItems.Clear();
        ClearError();
    }

    /// <summary>
    /// Fetches the weather data asynchronously.
    /// </summary>
    /// <returns></returns>
    [RelayCommand(AllowConcurrentExecutions = false, CanExecute = nameof(CanGetWeather))]
    private async Task GetWeatherAsync()
    {
        IsLoading = true;
        ClearError();

        try
        {
            // Create the weather service using the factory
            IWeatherService weatherService
                = weatherServiceFactory.CreateWeatherService(ApiKey);

            // Fetch the weather data
            WeatherResult result
                = await weatherService.GetWeatherDataAsync(
                    City, SelectedLanguage, SelectedUnit);

            // Check if the result is successful
            if (!result.IsSuccess)
            {
                SetError(result.ErrorMessage ?? Resources.Error_UnknownError);
                return;
            }

            // Update the ViewModel properties with the fetched data
            WeatherRoot = result.CurrentWeather;
            ForecastItems = [.. result.Forecast ?? []];
            CurrentAirPollution = result.CurrentAirPollution;
            AirPollutionForecast = [.. result.AirPollutionForecast ?? []];
        }
        finally
        {
            IsLoading = false;
        }
    }

    /// <summary>
    /// Determines whether the GetWeather command can be executed.
    /// </summary>
    /// <returns></returns>
    private bool CanGetWeather()
    {
        return !string.IsNullOrWhiteSpace(ApiKey);
    }
}
