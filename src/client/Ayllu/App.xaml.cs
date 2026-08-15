using UraniumUI.Material.Resources;
using APP = Microsoft.Maui.Controls.Application;
namespace Ayllu;

public partial class App : APP
{
    public App() => InitializeComponent();

    protected override Window CreateWindow(IActivationState? activationState) => new(new AppShell());
}
