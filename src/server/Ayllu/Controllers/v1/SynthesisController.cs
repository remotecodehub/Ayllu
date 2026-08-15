using Ayllu.Application.Synthesis.Commands;
using Ayllu.Application.Synthesis.Queries;
using Ayllu.Application.Synthesis.Requests;
using Ayllu.Application.Synthesis.Responses;
using Mediator.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ayllu.Controllers.v1;

[ApiController]
[Route("api/v1/dialectics/{dialecticId}/syntheses")]
[Authorize]
[Tags("Dialectics")]
public class SynthesisController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateSynthesis(string dialecticId, [FromBody] CreateSynthesisRequest request, CancellationToken cancellationToken)
        => Ok(await mediator.RequestAsync<CreateSynthesisCommand, SynthesisResponse>(new CreateSynthesisCommand(dialecticId, request, User.FindFirstValue(ClaimTypes.NameIdentifier)!)));

    [HttpGet]
    public async Task<IActionResult> List(string dialecticId, CancellationToken cancellationToken)
        => Ok(await mediator.RequestAsync<GetSynthesesByDialecticQuery, SynthesisListResponse>(new GetSynthesesByDialecticQuery(dialecticId, User.FindFirstValue(ClaimTypes.NameIdentifier)!)));
}
