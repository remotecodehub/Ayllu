using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Ayllu.Mobile.Presentation;

public partial class App : Microsoft.Maui.Controls.Application
{
    private readonly AppShell _appShell;
    private readonly ILogger<App> _logger;
    public App(AppShell appShell, ILogger<App> logger)
    {
        InitializeComponent();
        _appShell = appShell;
        _logger = logger;
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = new Window(_appShell);

        // Defer navigation until Shell is fully initialized
        _ = InitializeNavigationAsync();

        return window;
    }

    private async Task InitializeNavigationAsync()
    {
        try
        {
            await Task.Yield(); // guarantees Shell.Current exists

            await Shell.Current.GoToAsync("main");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to initialize navigation: {Message}", ex.Message);
            System.Diagnostics.Debug.WriteLine(ex);
        }
    }
}