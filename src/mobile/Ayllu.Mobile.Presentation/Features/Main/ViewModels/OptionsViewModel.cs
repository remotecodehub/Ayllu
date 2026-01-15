using Ayllu.Mobile.Application.Abstractions.Localization;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Localization;

namespace Ayllu.Mobile.Presentation.Features.Main.ViewModels;

public partial class OptionsViewModel(ILocalizationService localization, IStringLocalizer<OptionsViewModel> localizer) : ObservableObject
{
    private readonly ILocalizationService _localization = localization;
    private readonly IStringLocalizer<OptionsViewModel> _localizer = localizer;

    [ObservableProperty] public partial string TitleLabel { get; set; } 
    [ObservableProperty] public partial string LanguageLabel { get; set; } 
    [ObservableProperty] public partial string VersionLabel { get; set; } 
    
    [RelayCommand]
    private void Appearing()
    {
        TitleLabel = _localizer["OptionsPageTitle"];
        LanguageLabel = _localizer["LanguageLabel"];
        VersionLabel = _localizer["VersionLabel"];
    }
}
