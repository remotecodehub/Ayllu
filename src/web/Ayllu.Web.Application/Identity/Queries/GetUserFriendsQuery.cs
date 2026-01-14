using Ayllu.Web.Application.Identity.Responses;
using MediatR;

namespace Ayllu.Web.Application.Identity.Queries;

public sealed record GetUserFriendsQuery(string UserId) : IRequest<ICollection<ApplicationUserFriendResponse>>;