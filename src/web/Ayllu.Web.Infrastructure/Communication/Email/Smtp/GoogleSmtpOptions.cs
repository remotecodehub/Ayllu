
namespace Ayllu.Web.Infrastructure.Communication.Email.Smtp;

public sealed class GoogleSmtpOptions
{
    public const string SectionName = "Email:Smtp:Google";
    public string Host { get; init; } = default!;
    public int Port { get; init; }
    public bool UseSsl { get; init; }
    public string Username { get; init; } = default!;
    public string Password { get; init; } = default!;
    public string FromName { get; init; } = default!;
    public string FromEmail { get; init; } = default!;
}