using Ayllu.Mobile.Application.Abstractions.Localization;
using Ayllu.Mobile.Presentation.Common.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Ayllu.Mobile.Presentation.Features.Identity.ViewModels;

public partial class TwoFactorViewModel(ILocalizationService localization, IStringLocalizer<TwoFactorViewModel> localizer, ILogger<TwoFactorViewModel> logger) : ViewModelBase(localization)
{
    [ObservableProperty] public partial string? Code { get; set; }
    [ObservableProperty] public partial bool RememberDevice { get; set; } = false;
    [ObservableProperty] public partial string CodeLAbel { get; set; } = localizer["TwoFactorCodeLabel"];
    [ObservableProperty] public partial string RememberDeviceLabel { get; set; } = localizer["RememberDeviceLabel"];

    [RelayCommand]
    private async Task SubmitAsync()
    {
        logger.LogInformation("Forgot password requested");
        await Task.CompletedTask;
    }
}
