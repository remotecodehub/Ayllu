namespace Ayllu.Mobile.Application.Features.Identity.Results;

public sealed class TwoFactorResult
{
    public string SharedKey { get; set; } = string.Empty;
    public int RecoveryCodesLeft { get; set; }
    public string[] RecoveryCodes { get; set; } = [];
    public bool IsTwoFactorEnabled { get; set; }
    public bool IsMachineRemembered { get; set; }
}
