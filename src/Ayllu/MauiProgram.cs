using Microsoft.Extensions.Logging;
using MudBlazor.Services;
[assembly: System.Resources.NeutralResourcesLanguage("pt-BR")]

namespace Ayllu
{
    /// <summary>
    /// Main entry point for the application.
    /// </summary>
    public static class MauiProgram
    {
        /// <summary>
        /// Main method to create and configure the Maui application.
        /// </summary>
        /// <returns>The builded services and configurations ready to be executed on the platform</returns>
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddMudServices();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
