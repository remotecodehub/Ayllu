namespace Ayllu.Mobile.Application.Features.Identity.Dtos;

public sealed record TwoFactorDto(
    bool Enable, 
    string TwoFactorCode, 
    bool ResetSharedKey,
    bool ResetRecoveryCodes,
    bool ForgetMachine
);
