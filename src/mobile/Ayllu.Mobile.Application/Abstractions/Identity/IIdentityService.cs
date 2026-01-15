using Ayllu.Mobile.Application.Features.Identity.Dtos;
using Ayllu.Mobile.Application.Features.Identity.Results;

namespace Ayllu.Mobile.Application.Abstractions.Identity;

public interface IIdentityService
{
    // POST /api/v1/identity/register
    Task RegisterAsync(RegisterDto register, CancellationToken cancellationToken = default);

    // POST /api/v1/identity/login
    Task<AccessTokenResult?> LoginAsync(LoginDto login, CancellationToken cancellationToken = default);

    // POST /api/v1/identity/refresh
    Task<AccessTokenResult?> RefreshAsync(RefreshDto request, CancellationToken cancellationToken = default);

    // GET /api/v1/identity/confirmEmail
    Task ConfirmEmailAsync(string userId, string token, CancellationToken cancellationToken = default);

    // POST /api/v1/identity/resendConfirmationEmail
    Task ResendConfirmationEmailAsync(ResendConfirmationEmailDto request, CancellationToken cancellationToken = default);

    // POST /api/v1/identity/forgotPassword
    Task ForgotPasswordAsync(ForgotPasswordDto request, CancellationToken cancellationToken = default);

    // POST /api/v1/identity/resetPassword
    Task ResetPasswordAsync(ResetPasswordDto request, CancellationToken cancellationToken = default);

    // POST /api/v1/identity/manage/2fa
    Task<TwoFactorResult?> ManageTwoFactorAsync(TwoFactorDto request, CancellationToken cancellationToken = default);

    // GET /api/v1/identity/manage/info
    Task<InfoResult?> GetUserInfoAsync(CancellationToken cancellationToken = default);

    // POST /api/v1/identity/manage/info
    Task UpdateUserInfoAsync(InfoDto request, CancellationToken cancellationToken = default);

    // GET /api/v1/identity/me
    Task<CurrentUserResult?> GetCurrentUserAsync(CancellationToken cancellationToken = default);
}
