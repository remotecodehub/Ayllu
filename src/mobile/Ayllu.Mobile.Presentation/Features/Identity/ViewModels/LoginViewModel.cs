using Ayllu.Mobile.Application.Abstractions.Localization;
using Ayllu.Mobile.Presentation.Common.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Ayllu.Mobile.Presentation.Features.Identity.ViewModels;

public partial class LoginViewModel(ILocalizationService localization, IStringLocalizer<LoginViewModel> localizer, ILogger<LoginViewModel> logger) : ViewModelBase(localization)
{
    [ObservableProperty] public partial string EmailLabel { get; set; } = localizer["EmailLabel"];
    [ObservableProperty] public partial string PasswordLabel { get; set; } = localizer["PasswordLabel"];
    [ObservableProperty] public partial string LoginLabel { get; set; } = localizer["LoginLabel"];
    [ObservableProperty] public partial string Email { get; set; } = string.Empty;
    [ObservableProperty] public partial string Password { get; set; } = string.Empty;

    [RelayCommand]
    private async Task LoginAsync()
    {
        logger.LogInformation("Login attempt started");

        // TODO: Send MediatR command
        await Task.CompletedTask;
    }

    [RelayCommand]
    private async Task RegisterAsync()
    {
        await Shell.Current.GoToAsync("registerPage");
    }

    [RelayCommand]
    private async Task ForgotPasswordAsync()
    {
        await Shell.Current.GoToAsync("forgotPasswordPage");
    }
}
