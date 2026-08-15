using Ayllu.Application.Identity.Responses;
using MediatR;

namespace Ayllu.Application.Identity.Queries;

public sealed record GetUserFriendsQuery(string UserId) : IRequest<ICollection<ApplicationUserFriendResponse>>;