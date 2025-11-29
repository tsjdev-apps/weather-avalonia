using Avalonia.Data.Converters;
using AvaloniaWeather.Strings;
using System.Globalization;

namespace AvaloniaWeather.Converters;

/// <summary>
/// Converts DateTime to formatted string using localized resource
/// </summary>
public class DateTimeFormatConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is DateTime dateTime)
        {
            return string.Format(Resources.MeasuredAt_Label, dateTime);
        }

        return value?.ToString() ?? string.Empty;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
