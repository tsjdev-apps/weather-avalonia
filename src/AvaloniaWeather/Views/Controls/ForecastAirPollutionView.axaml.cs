using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using OpenWeatherMapSharp.Models;

namespace AvaloniaWeather.Views.Controls;

public partial class ForecastAirPollutionView : UserControl
{
    public ForecastAirPollutionView()
    {
        InitializeComponent();
    }

    public static readonly StyledProperty<ObservableCollection<AirPollutionEntry>> AirPollutionItemsProperty =
        AvaloniaProperty.Register<ForecastAirPollutionView, ObservableCollection<AirPollutionEntry>>(nameof(AirPollutionItems));

    public ObservableCollection<AirPollutionEntry> AirPollutionItems
    {
        get => GetValue(AirPollutionItemsProperty);
        set => SetValue(AirPollutionItemsProperty, value);
    }
}
