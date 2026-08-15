using Ayllu.Application.Antithesis.Commands;
using Ayllu.Application.Antithesis.Queries;
using Ayllu.Application.Antithesis.Requests;
using Ayllu.Application.Antithesis.Responses;
using Mediator.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ayllu.Controllers.v1;

[ApiController]
[Route("api/v1/dialectics/{dialecticId}/antitheses")]
[Tags("Dialectics")]
[Authorize]
public class AntithesisController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateAntithesis(string dialecticId, [FromBody] CreateAntithesisRequest request, CancellationToken cancellationToken)
        => Ok(await mediator.RequestAsync<CreateAntithesisCommand, AntithesisResponse>(new CreateAntithesisCommand(dialecticId, request, User.FindFirstValue(ClaimTypes.NameIdentifier)!)));

    [HttpGet]
    public async Task<IActionResult> List(string dialecticId, [FromQuery(Name = "publicOnly")] bool? publicOnly, CancellationToken cancellationToken)
        => Ok(await mediator.RequestAsync<GetAntithesesByDialecticQuery, AntithesisListResponse>(new GetAntithesesByDialecticQuery(dialecticId, User.FindFirstValue(ClaimTypes.NameIdentifier)!, publicOnly ?? true)));
}
