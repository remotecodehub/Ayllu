
using Ayllu.Composition.Extensions;
namespace Ayllu;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp() => MauiApp.CreateBuilder().BuildAyllu<App>();
}
