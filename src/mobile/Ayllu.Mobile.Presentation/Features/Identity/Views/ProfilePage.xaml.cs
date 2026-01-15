using Ayllu.Mobile.Presentation.Features.Identity.ViewModels;

namespace Ayllu.Mobile.Presentation.Features.Identity.Views;

public partial class ProfilePage : UraniumUI.Pages.UraniumContentPage
{
    public ProfilePage(ProfileViewModel vm)
    {
        InitializeComponent();
        this.BindingContext = vm;
    }

    private void Load(object sender, EventArgs e)
    {
        var vm = (ProfileViewModel)this.BindingContext;
        vm.LoadCommand.Execute(null);
    }
}
