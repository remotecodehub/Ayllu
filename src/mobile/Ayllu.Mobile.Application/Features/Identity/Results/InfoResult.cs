namespace Ayllu.Mobile.Application.Features.Identity.Results;

public sealed class InfoResult
{
    public string Email { get; set; } = string.Empty;
    public bool IsEmailConfirmed { get; set; }
}
