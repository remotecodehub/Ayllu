using Ayllu.Application.Dialectics.Commands;
using Ayllu.Application.Dialectics.Queries;
using Ayllu.Application.Dialectics.Requests;
using Ayllu.Application.Dialectics.Responses;
using Mediator.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ayllu.Controllers.v1;

/// <summary>
/// Controller for managing dialectics, providing endpoints for creating, closing, and retrieving dialectics.
/// </summary>
/// <param name="mediator"></param>
[ApiController]
[Route("api/v1/dialectics")]
[Tags("Dialectics")]
[Authorize]
public class DialecticsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateDialectic([FromBody] CreateDialecticRequest request, CancellationToken cancellationToken)
        => Ok(await mediator.RequestAsync<CreateDialecticCommand, CreateDialecticResponse>(
            new CreateDialecticCommand(request, User.FindFirstValue(ClaimTypes.NameIdentifier)!), cancellationToken));

    [HttpPost("{id}/close")]
    public async Task<IActionResult> CloseDialectic(string id, CancellationToken cancellationToken)
        => Ok(await mediator.RequestAsync<CloseDialecticCommand, DialecticResponse?>(
            new CloseDialecticCommand(id, User.FindFirstValue(ClaimTypes.NameIdentifier)!), cancellationToken));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
        => Ok(await mediator.RequestAsync<GetDialecticByIdQuery, DialecticResponse?>(
            new GetDialecticByIdQuery(id, User.FindFirstValue(ClaimTypes.NameIdentifier)!), cancellationToken));

    [HttpGet("mine")]
    public async Task<IActionResult> GetMyDialectics(CancellationToken cancellationToken)
        => Ok(await mediator.RequestAsync<GetMyDialecticsQuery, DialecticSummaryListResponse>(
            new GetMyDialecticsQuery(User.FindFirstValue(ClaimTypes.NameIdentifier)!), cancellationToken));

    [HttpGet("friends")]
    public async Task<IActionResult> GetFriendsDialectics(CancellationToken cancellationToken)
        => Ok(await mediator.RequestAsync<GetFriendsDialecticsQuery, DialecticListResponse>(
            new GetFriendsDialecticsQuery(User.FindFirstValue(ClaimTypes.NameIdentifier)!), cancellationToken));
}
