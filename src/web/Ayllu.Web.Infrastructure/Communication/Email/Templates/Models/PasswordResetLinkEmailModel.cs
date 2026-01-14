namespace Ayllu.Web.Infrastructure.Communication.Email.Templates.Models;

public sealed class PasswordResetLinkEmailModel
{
    public string UserName { get; init; } = null!;
    public string ResetLink { get; init; } = null!;
}
