namespace Ayllu.Mobile.Application.Features.Identity.Results;

public sealed class AccessTokenResult
{
    public string? TokenType { get; set; }
    public string? AccessToken { get; set; }
    public int ExpiresIn { get; set; }
    public string? RefreshToken { get; set; }
}
