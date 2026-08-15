using Ayllu.Application.Identity.Responses;
using Mediator.Net.Contracts;

namespace Ayllu.Application.Identity.Queries;

public sealed record GetUserFriendsQuery(string UserId) : IRequest<ICollection<ApplicationUserFriendResponse>>;
