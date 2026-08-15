namespace Ayllu.Infrastructure.Communication.Email.Templates.Models;

public sealed class ConfirmEmailModel
{
    public string UserName { get; init; } = default!;
    public string ConfirmationLink { get; init; } = default!;
}