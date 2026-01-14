using Ayllu.Mobile.Application.Abstractions.Localization;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Ayllu.Mobile.Presentation.Common.ViewModels;

public abstract partial class ViewModelBase : ObservableObject
{
    protected ILocalizationService Localization { get; }

    protected ViewModelBase(ILocalizationService localization)
    {
        Localization = localization;

        // Optional: auto-refresh when culture changes
        Localization.CultureChanged += OnCultureChanged;
    }

    private void OnCultureChanged()
    {
        OnPropertyChanged(string.Empty);
    }
}