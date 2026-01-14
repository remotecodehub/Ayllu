using Ayllu.Web.Application.Common.Abstractions.Identity;
using Ayllu.Web.Application.Identity.Queries;
using Ayllu.Web.Application.Identity.Responses;
using MediatR;

namespace Ayllu.Web.Application.Identity.Handlers;

public sealed class GetUserFriendsQueryHandler(IIdentityService @is) : IRequestHandler<GetUserFriendsQuery, ICollection<ApplicationUserFriendResponse>>
{
    public Task<ICollection<ApplicationUserFriendResponse>> Handle(GetUserFriendsQuery request, CancellationToken cancellationToken)
        => @is.GetUserFriendsAsync(request.UserId, cancellationToken);
}
