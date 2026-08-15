using Mopups.Hosting;
using UraniumUI.Icons.MaterialSymbols;
using InputKit.Shared.Controls;
using UraniumUI;
using CommunityToolkit.Maui;

namespace Ayllu.Composition.Extensions;

public static  class MauiAppBuilderExtensions
{
    extension(MauiAppBuilder builder)
    {
        public MauiApp BuildAyllu<T>() where T : Microsoft.Maui.Controls.Application
        {
            builder
                .UseMauiApp<T>()
                .UseMauiCommunityToolkit()
                .ConfigureMopups()
                .UseUraniumUI()
                .UseUraniumUIMaterial()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

                    fonts.AddMaterialSymbolsFonts();

                })
                .Services
                .AddMopupsDialogs();

            return builder.Build();
        }
    }
}
