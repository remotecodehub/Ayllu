using Ayllu.Application.Common.Interfaces;
using Ayllu.Application.CQRS.Commands;
using Ayllu.Application.DTO.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ayllu.Application.CQRS.Handlers
{
    public class LoginCommandHandler(IIdentityService identity, ILogger<LoginCommandHandler> logger) : IRequestHandler<LoginCommand, Response<LoginResponse>>
    {
        public async Task<Response<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var token = request.Token;
                // Remove o prefixo "Basic " se estiver presente
                if (token.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
                {
                    token = request.Token["Basic ".Length..].Trim();
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


                var (Success, Jwt, Expiration, RefreshToken, Email, PhoneNumber, UserName, Roles, Errors) = await identity.LoginAsync(parts[0], parts[1]);
                return new Response<LoginResponse>()
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
                    Message = Success ? "Login bem-sucedido" : string.Join(", ", Errors!),
                };
            }
            catch (Exception e)
            {
                logger.LogError(e, "Erro ao processar LoginCommand: {Message}", e.Message);
                throw new InvalidOperationException("Erro ao processar LoginCommand", e);
            }
        }
    }
}
