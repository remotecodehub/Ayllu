using Ayllu.Mobile.Application.Abstractions.Localization;
using Ayllu.Mobile.Presentation.Common.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Localization;

namespace Ayllu.Mobile.Presentation.Features.Main.ViewModels;

public partial class MainViewModel(ILocalizationService localization, IStringLocalizer<MainViewModel> localizer) : ViewModelBase(localization)
{

    [ObservableProperty]
    public partial string WelcomeLabel { get; set; } 
    [RelayCommand]
    private void Appearing()
    {
        WelcomeLabel = localizer["WelcomeMessage"];
    }

}
