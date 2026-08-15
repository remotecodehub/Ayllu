namespace Ayllu.Infrastructure.Communication.Email.Templates.Models;

public sealed class PasswordResetCodeEmailModel
{
    public string UserName { get; set; } = null!;
    public string ResetCode { get; set; } = null!;
}
