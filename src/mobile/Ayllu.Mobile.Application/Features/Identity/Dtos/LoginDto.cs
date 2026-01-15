namespace Ayllu.Mobile.Application.Features.Identity.Dtos;

public sealed record LoginDto(string Email, string Password, string? TwoFactorCode, string? TwoFactorRecoveryCode, bool UseCookies = false, bool UseSessionCoockies = false);
