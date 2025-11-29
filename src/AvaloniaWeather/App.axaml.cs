using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using AvaloniaWeather.Services;
using AvaloniaWeather.Services.Interfaces;
using AvaloniaWeather.Views;
using Microsoft.Extensions.DependencyInjection;

namespace AvaloniaWeather;

public partial class App : Application
{
    /// <summary>
    /// Gets the service provider for dependency injection.
    /// </summary>
    public static IServiceProvider? ServiceProvider { get; private set; }

    /// <summary>
    /// Initializes the application.
    /// </summary>
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    /// <summary>
    /// Called when the framework initialization is completed.
    /// </summary>
    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // STEP 1: Disable duplicate validation
            DisableAvaloniaDataAnnotationValidation();

            // STEP 2: Configure Dependency Injection Container
            ServiceCollection services = new();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();

            desktop.MainWindow = new MainWindow();
        }

        base.OnFrameworkInitializationCompleted();
    }

    /// <summary>
    /// Configures services for dependency injection.
    /// </summary>
    /// <param name="services">Service collection to register dependencies</param>
    private static void ConfigureServices(IServiceCollection services)
    {
        // Register Weather Service Factory (Singleton)
        services.AddSingleton<IWeatherServiceFactory, WeatherServiceFactory>();
    }

    /// <summary>
    /// Disables Avalonia's built-in DataAnnotations validation.
    /// </summary>
    private static void DisableAvaloniaDataAnnotationValidation()
    {
        // Find all DataAnnotationsValidationPlugin instances
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // Remove each plugin from the validators collection
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}
