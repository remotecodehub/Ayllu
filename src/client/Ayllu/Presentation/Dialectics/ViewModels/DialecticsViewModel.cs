using Microsoft.Extensions.Logging;

namespace Ayllu.Presentation.Dialectics.ViewModels;

public partial class DialecticsViewModel : ViewModelBase
{
    private readonly ILogger<DialecticsViewModel> logger;
    public DialecticsViewModel(ILogger<DialecticsViewModel> logger)
    {
        Title = "Dialéticas";
        IsBusy = false;
        this.logger = logger;
    }    
}
