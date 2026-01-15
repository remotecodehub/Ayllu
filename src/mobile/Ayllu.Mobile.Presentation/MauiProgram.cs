using Ayllu.Mobile.Presentation.Features.Identity.ViewModels;
using Ayllu.Mobile.Presentation.Features.Identity.Views;
using Ayllu.Mobile.Presentation.Features.Main.ViewModels; 
using Ayllu.Mobile.Presentation.Views.Main;
using CommunityToolkit.Maui;
using InputKit.Handlers;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using UraniumUI;

namespace Ayllu.Mobile.Presentation;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseUraniumUI()
            .UseUraniumUIMaterial()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFontAwesomeIconFonts();
                fonts.AddMaterialSymbolsFonts();
            })
            .ConfigureMauiHandlers(handlers =>
            {
                handlers.AddInputKitHandlers(); 
            });

        builder.Services.AddLocalization(options =>
        {
            options.ResourcesPath = "Resources";
        });
        builder.Services.AddSingleton<IStringLocalizerFactory, ResourceManagerStringLocalizerFactory>();

        builder.Services.AddSingleton<IStringLocalizer>(sp =>
        {
            var factory = sp.GetRequiredService<IStringLocalizerFactory>();
            return factory.Create(
                "Localization.Strings",
                typeof(App).Assembly.FullName!);
        });
        builder.Services.AddSingleton<AppShell>();
        builder.Services.AddSingleton<App>();
        builder.Services.AddSingletonWithShellRoute<MainPage, MainViewModel>("main");
        builder.Services.AddSingletonWithShellRoute<ForgotPasswordPage, ForgotPasswordViewModel>("forgotPassword");
        builder.Services.AddSingletonWithShellRoute<LoginPage, LoginViewModel>("login");
        builder.Services.AddSingletonWithShellRoute<ProfilePage, ProfileViewModel>("profile");
        builder.Services.AddSingletonWithShellRoute<RegisterPage, RegisterViewModel>("register");
        builder.Services.AddSingletonWithShellRoute<TwoFactorPage, TwoFactorViewModel>("twoFactor");

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
