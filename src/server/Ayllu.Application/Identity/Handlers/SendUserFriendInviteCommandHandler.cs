using Ayllu.Application.Common.Abstractions.Identity;
using Ayllu.Application.Identity.Commands;
using Ayllu.Application.Identity.Responses;
using Mediator.Net.Context;
using Mediator.Net.Contracts;

namespace Ayllu.Application.Identity.Handlers;

public sealed class SendUserFriendInviteCommandHandler(IIdentityService identityService)
    : IRequestHandler<SendUserFriendInviteCommand, ApplicationUserFriendResponse>
{
    public async Task<ApplicationUserFriendResponse> Handle(IReceiveContext<SendUserFriendInviteCommand> context, CancellationToken cancellationToken)
    {
        return await identityService.SendInviteUserFriendAsync(context.Message.UserId, context.Message.request.FriendId, cancellationToken);
    }
}
