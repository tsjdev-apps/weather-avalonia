using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using OpenWeatherMapSharp.Models;
using OpenWeatherMapSharp.Models.Enums;

namespace AvaloniaWeather.Views.Controls;

public partial class ForecastWeatherView : UserControl
{
    public ForecastWeatherView()
    {
        InitializeComponent();
    }

    public static readonly StyledProperty<ObservableCollection<ForecastItem>> ForecastItemsProperty =
        AvaloniaProperty.Register<ForecastWeatherView, ObservableCollection<ForecastItem>>(nameof(ForecastItems));

    public ObservableCollection<ForecastItem> ForecastItems
    {
        get => GetValue(ForecastItemsProperty);
        set => SetValue(ForecastItemsProperty, value);
    }

    public static readonly StyledProperty<Unit> UnitProperty =
       AvaloniaProperty.Register<ForecastWeatherView, Unit>(nameof(Unit), defaultValue: Unit.Standard);

    public Unit Unit
    {
        get => GetValue(UnitProperty);
        set => SetValue(UnitProperty, value);
    }

    private void UserControl_ActualThemeVariantChanged(object? sender, System.EventArgs e)
    {
    }
}
