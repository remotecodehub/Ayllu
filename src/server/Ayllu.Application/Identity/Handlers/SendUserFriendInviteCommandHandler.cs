using Ayllu.Application.Common.Abstractions.Identity;
using Ayllu.Application.Identity.Commands;
using Ayllu.Application.Identity.Responses;
using Mediator.Net.Context;
using Mediator.Net.Contracts;

namespace Ayllu.Application.Identity.Handlers;

public sealed class SendUserFriendInviteCommandHandler(IIdentityService identityService)
    : IRequestHandler<SendUserFriendInviteCommand, UserFriendInviteSentResponse>
{
    public async Task<UserFriendInviteSentResponse> Handle(IReceiveContext<SendUserFriendInviteCommand> context, CancellationToken cancellationToken)
    {
        await identityService.SendInviteUserFriendAsync(context.Message.UserId, context.Message.request.FriendId, cancellationToken);
        return new UserFriendInviteSentResponse();
    }
}
