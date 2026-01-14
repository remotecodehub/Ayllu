namespace Ayllu.Mobile.Application.Features.Identity.Dtos;

public sealed record ResetPasswordDto(string Email, string NewPassword, string ResetCode);