using Ayllu.Mobile.Application;
using Ayllu.Mobile.Application.Abstractions.Localization;
using Ayllu.Mobile.Infrastructure;
using Ayllu.Mobile.Infrastructure.Localization.Service;
using Microsoft.Extensions.Localization;

namespace Ayllu.Mobile.Composition;

// All the code in this file is included in all platforms.
public static class Composition
{
    public static IServiceCollection AddAylluApp(this MauiAppBuilder builder)
    {
        builder.AddApplication();
        builder.AddInfrastructure();
        return builder.Services;
    }
    private static IServiceCollection AddApplication(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<IStringLocalizer<ApplicationAssemblyMarker>>();

        return builder.Services;
    }
    private static IServiceCollection AddInfrastructure(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<IStringLocalizer<InfrastructureAssemblyMarker>>();
        builder.Services.AddSingleton<ILocalizationService, LocalizationService>();
        return builder.Services;
    }
}
