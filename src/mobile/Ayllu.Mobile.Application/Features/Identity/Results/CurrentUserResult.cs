namespace Ayllu.Mobile.Application.Features.Identity.Results;

public sealed class CurrentUserResult
{
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string[] Roles { get; set; } = [];
}
