using Ayllu.Mobile.Client.Models;

namespace Ayllu.Mobile.Infrastructure.HttpClient.Identity.Abstractions;

public interface IIdentityService
{
    // POST /api/v1/identity/register
    Task RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default);

    // POST /api/v1/identity/login
    Task<AccessTokenResponse?> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);

    // POST /api/v1/identity/refresh
    Task<AccessTokenResponse?> RefreshAsync(RefreshRequest request, CancellationToken cancellationToken = default);

    // GET /api/v1/identity/confirmEmail
    Task ConfirmEmailAsync(string userId, string token, CancellationToken cancellationToken = default);

    // POST /api/v1/identity/resendConfirmationEmail
    Task ResendConfirmationEmailAsync(ResendConfirmationEmailRequest request, CancellationToken cancellationToken = default);

    // POST /api/v1/identity/forgotPassword
    Task ForgotPasswordAsync(ForgotPasswordRequest request, CancellationToken cancellationToken = default);

    // POST /api/v1/identity/resetPassword
    Task ResetPasswordAsync(ResetPasswordRequest request, CancellationToken cancellationToken = default);

    // POST /api/v1/identity/manage/2fa
    Task<TwoFactorResponse?> ManageTwoFactorAsync(TwoFactorRequest request, CancellationToken cancellationToken = default);

    // GET /api/v1/identity/manage/info
    Task<InfoResponse?> GetUserInfoAsync(CancellationToken cancellationToken = default);

    // POST /api/v1/identity/manage/info
    Task UpdateUserInfoAsync(InfoRequest request, CancellationToken cancellationToken = default);

    // GET /api/v1/identity/me
    //Task<ProfileResponse?> GetCurrentUserAsync(CancellationToken cancellationToken = default);
}
