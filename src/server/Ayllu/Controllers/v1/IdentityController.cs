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

[Route("api/v1/identity")]
[ApiController]
[Tags("Identity")]
public class IdentityController(IMediator mediator) : ControllerBase
{
    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUserAsync(CancellationToken cancellationToken)
        => Ok(Result<ApplicationUserResponse>.Success(
            await mediator.RequestAsync<GetCurrentUserQuery, ApplicationUserResponse>(
            new GetCurrentUserQuery(User.FindFirstValue(ClaimTypes.NameIdentifier)!), cancellationToken)));

    [HttpPost("me")]
    [Authorize]
    public async Task<IActionResult> UpdateCurrentUserAsync([FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
        => Ok(Result<ApplicationUserResponse>.Success(
            await mediator.RequestAsync<UpdateUserCommand, ApplicationUserResponse>(
            new UpdateUserCommand(request, User.FindFirstValue(ClaimTypes.NameIdentifier)!), cancellationToken)));

}
