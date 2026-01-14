namespace Ayllu.Mobile.Application.Features.Identity.Dtos;

public sealed record ConfirmEmailDto(string UserId, string Code, string ChangedEmail);
