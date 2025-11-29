using Avalonia;
using Avalonia.Controls;
using OpenWeatherMapSharp.Models;
using OpenWeatherMapSharp.Models.Enums;

namespace AvaloniaWeather.Views.Controls;

public partial class CurrentWeatherView : UserControl
{
    public CurrentWeatherView()
    {
        InitializeComponent();
    }

    public static readonly StyledProperty<WeatherRoot?> WeatherRootProperty =
        AvaloniaProperty.Register<CurrentWeatherView, WeatherRoot?>(nameof(WeatherRoot));

    public WeatherRoot? WeatherRoot
    {
        get => GetValue(WeatherRootProperty);
        set => SetValue(WeatherRootProperty, value);
    }

    public static readonly StyledProperty<Unit> UnitProperty =
        AvaloniaProperty.Register<CurrentWeatherView, Unit>(nameof(Unit), defaultValue: Unit.Standard);

    public Unit Unit
    {
        get => GetValue(UnitProperty);
        set => SetValue(UnitProperty, value);
    }
}
