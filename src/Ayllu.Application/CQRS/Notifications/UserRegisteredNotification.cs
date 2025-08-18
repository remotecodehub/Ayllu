using Ayllu.Application.DTO.Responses;
using MediatR;

namespace Ayllu.Application.CQRS.Notifications
{
    public record UserRegisteredNotification (string Token, TaskCompletionSource<Response<LoginResponse>> CompletionSource) : INotification;
}
