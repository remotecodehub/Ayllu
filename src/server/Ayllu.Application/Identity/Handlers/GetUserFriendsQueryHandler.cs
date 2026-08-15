using Ayllu.Application.Common.Abstractions.Identity;
using Ayllu.Application.Identity.Queries;
using Ayllu.Application.Identity.Responses;
using MediatR;

namespace Ayllu.Application.Identity.Handlers;

public sealed class GetUserFriendsQueryHandler(IIdentityService @is) : IRequestHandler<GetUserFriendsQuery, ICollection<ApplicationUserFriendResponse>>
{
    public Task<ICollection<ApplicationUserFriendResponse>> Handle(GetUserFriendsQuery request, CancellationToken cancellationToken)
        => @is.GetUserFriendsAsync(request.UserId, cancellationToken);
}
