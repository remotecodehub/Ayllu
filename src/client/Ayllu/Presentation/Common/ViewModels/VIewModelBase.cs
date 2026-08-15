namespace Ayllu.Presentation.Common.ViewModels;

public abstract partial class ViewModelBase : ObservableObject 
{
    [ObservableProperty]
    public partial bool IsBusy { get; set; } = false;
    [ObservableProperty]
    public partial string Title{ get; set; } = string.Empty;
}
