using Ayllu.Mobile.Presentation.Features.Main.ViewModels;
using UraniumUI.Pages;

namespace Ayllu.Mobile.Presentation.Views.Main;

public partial class MainPage : UraniumContentPage
{ 

	public MainPage(MainViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ((MainViewModel)(this.BindingContext)).AppearingCommand.Execute(null);
    }
}
