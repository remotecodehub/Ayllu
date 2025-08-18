using Ayllu.Application.Common.Interfaces;
using Ayllu.Application.CQRS.Commands;
using Ayllu.Application.CQRS.Notifications;
using Ayllu.Application.DTO.Responses;
using Ayllu.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace Ayllu.Application.CQRS.Handlers;

public class RegisterCommandHandler(IIdentityService identityService, IMediator mediator) : IRequestHandler<RegisterCommand, Response<LoginResponse>>
{
    public async Task<Response<LoginResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var (Success, _, statusCode, Errors) = await identityService.RegisterAsync(request.UserName,
                request.Email,
                request.Password,
                request.Name,
                request.LastName,
                request.PhoneNumber
            );

            if (!Success)
                return new Response<LoginResponse>() { Data = null!, Message = string.Join(", ", Errors!), StatusCode = statusCode, Success = false };

            // 2. Prepara o TaskCompletionSource para capturar o retorno do login
            TaskCompletionSource<Response<LoginResponse>> tcs = new();
            var credentials = $"{request.Email}:{request.Password}";
            var bytes = Encoding.UTF8.GetBytes(credentials);
            var base64 = Convert.ToBase64String(bytes);
            

            // 3. Publica a notification
            await mediator.Publish(new UserRegisteredNotification(
                 $"Basic {base64}",
                tcs
            ), cancellationToken);

            // 4. Aguarda o resultado do handler de login
            var loginResponse = await tcs.Task;
            return loginResponse;
        }
        catch (Exception e)
        {

            throw;
        }
    }
}
