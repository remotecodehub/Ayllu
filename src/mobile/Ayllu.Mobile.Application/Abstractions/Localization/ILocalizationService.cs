using System.Globalization;

namespace Ayllu.Mobile.Application.Abstractions.Localization;

public interface ILocalizationService
{
    CultureInfo CurrentCulture { get; }
    void SetCulture(string culture);
    void UseSystemCulture();
    event Action CultureChanged;
}
