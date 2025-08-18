namespace Ayllu.Application.DTO.Responses;

public class LoginResponse
{
    public string Jwt { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
    public string RefreshToken { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public IEnumerable<string> Roles { get; set; } = [];
}
