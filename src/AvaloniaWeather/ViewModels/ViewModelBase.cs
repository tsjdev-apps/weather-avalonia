using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaWeather.ViewModels;

public abstract partial class ViewModelBase : ObservableObject
{
    /// <summary>
    /// Indicates whether a loading operation is in progress.
    /// </summary>
    [ObservableProperty]
    private bool _isLoading;

    /// <summary>
    /// Holds the error message, if any.
    /// </summary>
    [ObservableProperty]
    private string? _errorMessage;

    /// <summary>
    /// Indicates whether there is an error.
    /// </summary>
    [ObservableProperty]
    private bool _hasError;


    /// <summary>
    /// Sets the error message and updates the HasError flag.
    /// </summary>
    /// <param name="message"></param>
    protected void SetError(string message)
    {
        ErrorMessage = message;
        HasError = true;
    }

    /// <summary>
    /// Clears the error message and resets the HasError flag.
    /// </summary>
    protected void ClearError()
    {
        ErrorMessage = null;
        HasError = false;
    }
}
