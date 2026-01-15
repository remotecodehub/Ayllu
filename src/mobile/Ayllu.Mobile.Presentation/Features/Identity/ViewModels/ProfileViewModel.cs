using Ayllu.Mobile.Application.Abstractions.Localization;
using Ayllu.Mobile.Presentation.Common.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Ayllu.Mobile.Presentation.Features.Identity.ViewModels;

public partial class ProfileViewModel(ILocalizationService localization, IStringLocalizer<ProfileViewModel> localizer, ILogger<ProfileViewModel> logger) : ViewModelBase(localization)
{
    [ObservableProperty] public partial string? FirstName { get; set; }
    [ObservableProperty] public partial string? LastName { get; set; }
    [ObservableProperty] public partial string? Email { get; set; }
    [ObservableProperty] public partial string? PhoneNumber { get; set; }
    [ObservableProperty] public partial string? Username { get; set; }
    [ObservableProperty] public partial string? Culture { get; set; }
    [ObservableProperty] public partial string FirstNameLabel { get; set; } = localizer["FirstNameLabel"];
    [ObservableProperty] public partial string LastNameLabel { get; set; } = localizer["LastNameLabel"];
    [ObservableProperty] public partial string EmailLabel { get; set; } = localizer["EmailLabel"];
    [ObservableProperty] public partial string PhoneNumberLabel { get; set; } = localizer["PhoneLabel"];
    [ObservableProperty] public partial string UsernameLabel { get; set; } = localizer["UsernameLabel"];
    [ObservableProperty] public partial string LamguageLabel { get; set; } = localizer["LamguageLabel"];

    [RelayCommand]
    private async Task LoadAsync()
    {
        logger.LogInformation("Loading user profile");
        await Task.CompletedTask; 
    }

    [RelayCommand]
    private async Task UpdateAsync()
    {
        logger.LogInformation("updating user profile");
        await Task.CompletedTask; 
    }
}
