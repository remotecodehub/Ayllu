using Ayllu.Application.Common.Interfaces;
using Ayllu.Application.CQRS.Notifications;
using Ayllu.Application.DTO.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace Ayllu.Application.CQRS.Handlers;

public class UserRegisteredNotificationHandler(IIdentityService identityService) : INotificationHandler<UserRegisteredNotification>
{
    public async Task Handle(UserRegisteredNotification notification, CancellationToken cancellationToken)
    {
        var token = notification.Token;
        // Remove o prefixo "Basic " se estiver presente
        if (token.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
        {
            token = notification.Token["Basic ".Length..].Trim();
        }

        // Decodifica de Base64
        var decodedBytes = Convert.FromBase64String(token);
        var decodedString = Encoding.UTF8.GetString(decodedBytes);

        // Divide em usuário e senha
        var parts = decodedString.Split(':', 2);
        if (parts.Length != 2)
        {
            throw new FormatException("Token inválido. Esperado formato 'username:password'.");
        }

        // Executa o login automático
        var (Success, Jwt, Expiration, RefreshToken, Email, PhoneNumber, UserName, Roles, Errors) = await identityService.LoginAsync(parts[0], parts[1]);

        // Seta o resultado no TaskCompletionSource
        notification.CompletionSource.SetResult(new Response<LoginResponse>()
        {
            Data = new LoginResponse
            {
                Jwt = Jwt,
                Expiration = Expiration,
                RefreshToken = RefreshToken,
                Email = Email!,
                PhoneNumber = PhoneNumber!,
                UserName = UserName!,
                Roles = Roles

            },
            Success = Success,
            StatusCode = Success ? StatusCodes.Status200OK : StatusCodes.Status401Unauthorized,
            Message = Success ? "Login successful" : string.Join(", ", Errors!)
        });
    }
}