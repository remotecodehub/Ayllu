using Ayllu.Mobile.Presentation.Features.Identity.ViewModels;

namespace Ayllu.Mobile.Presentation.Features.Identity.Views;

public partial class RegisterPage : UraniumUI.Pages.UraniumContentPage
{
    public RegisterPage(RegisterViewModel vm)
    {
        InitializeComponent();
        this.BindingContext = vm;
    }
}
