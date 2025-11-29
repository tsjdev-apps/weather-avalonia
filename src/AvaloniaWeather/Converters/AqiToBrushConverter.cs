using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace AvaloniaWeather.Converters;

/// <summary>
/// Converts an Air Quality Index (AQI) value to a corresponding Brush color.
/// </summary>
public class AqiToBrushConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not int aqi)
        {
            return Brushes.Transparent;
        }

        return aqi switch
        {
            1 => Brushes.Green,
            2 => Brushes.YellowGreen,
            3 => Brushes.Orange,
            4 => Brushes.Red,
            5 => Brushes.Purple,
            _ => Brushes.Gray
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
