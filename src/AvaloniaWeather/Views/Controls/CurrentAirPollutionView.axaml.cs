using Avalonia;
using Avalonia.Controls;
using OpenWeatherMapSharp.Models;

namespace AvaloniaWeather.Views.Controls;

public partial class CurrentAirPollutionView : UserControl
{
    public CurrentAirPollutionView()
    {
        InitializeComponent();
    }

    public static readonly StyledProperty<AirPollutionEntry?> AirPollutionProperty =
        AvaloniaProperty.Register<CurrentAirPollutionView, AirPollutionEntry?>(nameof(AirPollution));

    public AirPollutionEntry? AirPollution
    {
        get => GetValue(AirPollutionProperty);
        set => SetValue(AirPollutionProperty, value);
    }
}
