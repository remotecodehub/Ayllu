using Ayllu.Mobile.Application.Abstractions.Localization;
using System.Globalization;

namespace Ayllu.Mobile.Infrastructure.Localization.Service;


public sealed class LocalizationService : ILocalizationService
{
    public CultureInfo CurrentCulture => CultureInfo.CurrentUICulture;
    private static readonly string[] _supportedCultures = ["en-US", "pt-BR"];

    public event Action? CultureChanged;

    public void SetCulture(string culture)
    {
        if (!_supportedCultures.Any(c => c.Equals(culture, StringComparison.OrdinalIgnoreCase)))
        {
            culture = "pt-BR";
        }

        var ci = new CultureInfo(culture);

        CultureInfo.DefaultThreadCurrentCulture = ci;
        CultureInfo.DefaultThreadCurrentUICulture = ci;
        CultureInfo.CurrentCulture = ci;
        CultureInfo.CurrentUICulture = ci;

        Thread.CurrentThread.CurrentCulture = ci;
        Thread.CurrentThread.CurrentUICulture = ci;
        CultureChanged?.Invoke();
    }

    public void UseSystemCulture()
    {
        var ci = CultureInfo.InstalledUICulture;
        SetCulture(ci.Name);
    }
}
