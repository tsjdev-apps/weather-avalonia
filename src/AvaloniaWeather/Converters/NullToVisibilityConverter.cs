using System.Globalization;
using Avalonia.Data.Converters;

namespace AvaloniaWeather.Converters;

/// <summary>
/// Converts a null value to a boolean indicating visibility.
/// </summary>
public class NullToVisibilityConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value != null;

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
