using Ayllu.Application.Common.Abstractions.Identity;
using Ayllu.Application.Identity.Queries;
using Ayllu.Application.Identity.Responses;
using Mediator.Net.Context;
using Mediator.Net.Contracts;

namespace Ayllu.Application.Identity.Handlers;

public sealed class GetUserFriendsQueryHandler(IIdentityService @is) : IRequestHandler<GetUserFriendsQuery, ApplicationUserFriendListResponse>
{
    public async Task<ApplicationUserFriendListResponse> Handle(IReceiveContext<GetUserFriendsQuery> context, CancellationToken cancellationToken)
        => new (await @is.GetUserFriendsAsync(context.Message.UserId, cancellationToken));
}
