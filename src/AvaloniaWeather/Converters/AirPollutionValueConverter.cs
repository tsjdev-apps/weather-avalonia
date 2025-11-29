using Avalonia.Data.Converters;
using System.Globalization;

namespace AvaloniaWeather.Converters;

/// <summary>
/// Converts air pollution values to formatted strings with µg/m³ unit
/// </summary>
public class AirPollutionValueConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is double doubleValue)
        {
            return $"{doubleValue:F1} µg/m³";
        }

        return value?.ToString() ?? string.Empty;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
