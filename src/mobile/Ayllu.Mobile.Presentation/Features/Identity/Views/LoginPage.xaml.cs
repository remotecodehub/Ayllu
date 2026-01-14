using Ayllu.Mobile.Presentation.Features.Identity.ViewModels;

namespace Ayllu.Mobile.Presentation.Features.Identity.Views;

public partial class LoginPage : UraniumUI.Pages.UraniumContentPage
{
    public LoginPage(LoginViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
