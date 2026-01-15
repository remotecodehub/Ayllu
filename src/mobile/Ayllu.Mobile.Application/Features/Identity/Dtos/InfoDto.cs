namespace Ayllu.Mobile.Application.Features.Identity.Dtos;

public sealed record InfoDto(bool Enable, string TwoFactorCode, bool ResetSharedKey, bool ResetRecoveryCodes, bool ForgetMachine );