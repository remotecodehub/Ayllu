namespace Ayllu.Presentation.Common.ViewModels;

public partial class VIewModelBase : ObservableObject 
{
    [ObservableProperty]
    public partial bool IsBusy { get; set; } = false;
    [ObservableProperty]
    public partial string Title{ get; set; } = string.Empty;
}
