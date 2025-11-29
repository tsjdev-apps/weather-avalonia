using System.Globalization;
using Avalonia.Data.Converters;

namespace AvaloniaWeather.Converters;

/// <summary>
/// Converts a distance in meters to a formatted string in kilometers.
/// </summary>
public class MetersToKmConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is double meters ? $"{meters / 1000:F1-} km" : "-";

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
