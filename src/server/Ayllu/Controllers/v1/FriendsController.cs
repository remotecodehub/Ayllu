using Ayllu.Application.Common.Results;
using Ayllu.Application.Identity.Commands;
using Ayllu.Application.Identity.Queries;
using Ayllu.Application.Identity.Requests;
using Ayllu.Application.Identity.Responses;
using Mediator.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ayllu.Controllers.v1;

[Route("api/v1/identity/me/friends")]
[ApiController]
[Tags("Identity")]
public class FriendsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddFriendAsync([FromBody] SendUserFriendInviteRequest request, CancellationToken cancellationToken)
        => Ok(Result<UserFriendInviteSentResponse>.Success(await mediator.RequestAsync<SendUserFriendInviteCommand, UserFriendInviteSentResponse>(new SendUserFriendInviteCommand(request, User.FindFirstValue(ClaimTypes.NameIdentifier)!))));

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> ListFriendsAsync(CancellationToken cancellationToken)
        => Ok(Result<ICollection<ApplicationUserFriendResponse>>.Success(await mediator.RequestAsync<GetUserFriendsQuery, ICollection<ApplicationUserFriendResponse>>(new GetUserFriendsQuery(User.FindFirstValue(ClaimTypes.NameIdentifier)!))));
}
