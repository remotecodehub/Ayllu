using System;
using System.Collections.Generic;
using System.Text;

namespace Ayllu.Web.Application.Identity.Responses;

public sealed class AuthenticationResponse(string accessToken, string refreshToken, DateTime accessTokenExpiration, DateTime refreshTokenExpiration)
{
    public string AccessToken { get; init; } = accessToken;
    public string RefreshToken { get; init; } = refreshToken;
    public DateTime AccessTokenExpiration { get; init; } = accessTokenExpiration;
    public DateTime RefreshTokenExpiration { get; init; } = refreshTokenExpiration;
}
