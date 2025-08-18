namespace Ayllu.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<(bool Success, string? UserId, int StatusCode, IEnumerable<string>? Errors)> RegisterAsync(string username, string email, string password, string name, string lastName, string phoneNumber);
    Task<(bool Success, string Jwt, DateTime Expiration, string RefreshToken, string? Email, string? PhoneNumber, string? UserName, IEnumerable<string> Roles, IEnumerable<string>? Errors)> LoginAsync(string key, string password);
    Task LogoutAsync(string jwt);
    Task<(bool Success, string? Token, IEnumerable<string>? Errors)> ForgotPasswordAsync(string email);
    Task<(bool Success, IEnumerable<string>? Errors)> ResetPasswordAsync(string email, string token, string newPassword);
}