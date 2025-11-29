using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;
using OpenWeatherMapSharp.Models.Enums;

namespace AvaloniaWeather.Converters;

/// <summary>
/// Converts a numeric value and a unit type to a formatted string with the appropriate unit suffix.
/// </summary>
public class UnitSuffixConverter : IMultiValueConverter
{
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values is null || values.Count < 2 || values[0] is not double val || values[1] is not Unit unit)
        {
            return AvaloniaProperty.UnsetValue;
        }

        string type = parameter?.ToString() ?? string.Empty;

        return type switch
        {
            "temperature" => unit switch
            {
                Unit.Standard => $"{val:F1} K",
                Unit.Metric => $"{val:F1} °C",
                Unit.Imperial => $"{val:F1} °F",
                _ => AvaloniaProperty.UnsetValue
            },
            "wind" => unit switch
            {
                Unit.Standard => $"{val:F1} m/s",
                Unit.Metric => $"{val:F1} m/s",
                Unit.Imperial => $"{val:F1} mph",
                _ => AvaloniaProperty.UnsetValue
            },
            "precip" => unit switch
            {
                Unit.Standard => $"{val:F1} mm/h",
                Unit.Metric => $"{val:F1} mm/h",
                Unit.Imperial => $"{val:F1} mm/h",
                _ => AvaloniaProperty.UnsetValue
            },
            _ => throw new NotImplementedException()
        };
    }
}
