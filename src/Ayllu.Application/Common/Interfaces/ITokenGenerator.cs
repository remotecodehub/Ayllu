namespace Ayllu.Application.Common.Interfaces;

public interface ITokenGenerator
{
    bool IsTokenInvalidated(string jwt);
    void Invalidate(string jwt);
    (string jwt, DateTime expirationDate, string refreshToken) GenerateToken(
        string username,
        string email,
        string organization,
        string phoneNumber,
        IList<string> roles,
        IList<string> organizationRoles);
}
