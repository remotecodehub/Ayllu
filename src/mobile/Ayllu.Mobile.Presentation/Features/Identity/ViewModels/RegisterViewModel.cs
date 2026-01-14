using Ayllu.Mobile.Application.Abstractions.Localization;
using Ayllu.Mobile.Presentation.Common.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input; 
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Ayllu.Mobile.Presentation.Features.Identity.ViewModels;

public partial class RegisterViewModel(ILocalizationService localization, IStringLocalizer<RegisterViewModel> localizer, ILogger<RegisterViewModel> logger) : ViewModelBase(localization)
{

    [ObservableProperty] public partial string Email { get; set; } = string.Empty;
    [ObservableProperty] public partial string Password { get; set; } = string.Empty;
    [ObservableProperty] public partial string ConfirmPassword { get; set; } = string.Empty;
    [ObservableProperty] public partial string EmailLabel { get; set; } = localizer["EmailLabel"];
    [ObservableProperty] public partial string PasswordLabel { get; set; } = localizer["PasswordLabel"];
    [ObservableProperty] public partial string ConfirmPasswordLabel { get; set; } = localizer["ConfirmPasswordLabel"];

    [RelayCommand]
    private async Task RegisterAsync()
    {
        logger.LogInformation("Register started");
        await Task.CompletedTask;
    }
}
