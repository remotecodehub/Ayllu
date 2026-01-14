using Ayllu.Web.Application.Identity.Requests;
using Ayllu.Web.Application.Identity.Responses;
using MediatR;

namespace Ayllu.Web.Application.Identity.Commands;

public sealed record SendUserFriendInviteCommand(SendUserFriendInviteRequest request, string UserId) : IRequest<UserFriendInviteSentResponse>;
