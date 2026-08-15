using Ayllu.Application.Identity.Requests;
using Ayllu.Application.Identity.Responses;
using Mediator.Net.Contracts;

namespace Ayllu.Application.Identity.Commands;

public sealed record SendUserFriendInviteCommand(SendUserFriendInviteRequest request, string UserId) : IRequest<UserFriendInviteSentResponse>;
