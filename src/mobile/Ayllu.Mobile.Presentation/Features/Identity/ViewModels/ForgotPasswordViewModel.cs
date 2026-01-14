using Ayllu.Mobile.Application.Abstractions.Localization;
using Ayllu.Mobile.Presentation.Common.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Ayllu.Mobile.Presentation.Features.Identity.ViewModels;

public partial class ForgotPasswordViewModel(ILocalizationService localization, IStringLocalizer<ForgotPasswordViewModel> localizer, ILogger<ForgotPasswordViewModel> logger) : ViewModelBase(localization)
{
    [ObservableProperty]
    public partial string EmailLabel { get; set; } = localizer["EmailLabel"];
    [ObservableProperty]
    public partial string SubmitLabel { get; set; } = localizer["LoginLabel"];
    [ObservableProperty]
    public partial string Email { get; set; } = string.Empty;

    [RelayCommand]
    private async Task SubmitAsync()
    {
        logger.LogInformation("Forgot password requested");
        await Task.CompletedTask;
    }
}
