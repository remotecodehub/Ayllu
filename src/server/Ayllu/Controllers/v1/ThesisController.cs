using Ayllu.Application.Thesis.Commands;
using Ayllu.Application.Thesis.Requests;
using Ayllu.Application.Thesis.Responses;
using Mediator.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ayllu.Controllers.v1;

[ApiController]
[Route("api/v1/dialectics/{dialecticId}/thesis")]
[Tags("Dialectics")]
[Authorize]
public class ThesisController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PublishThesis([FromBody] CreateThesisRequest request, string dialecticId, CancellationToken cancellationToken)
        => Ok(await mediator.RequestAsync<CreateThesisCommand, ThesisResponse>(new CreateThesisCommand(dialecticId, request, User.FindFirstValue(ClaimTypes.NameIdentifier)!)));

    [HttpPut("{thesisId}")]
    public async Task<IActionResult> UpdateThesis([FromBody] UpdateThesisRequest request, string dialecticId, string thesisId, CancellationToken cancellationToken)
        => Ok(await mediator.RequestAsync<UpdateThesisCommand, ThesisResponse>(new UpdateThesisCommand(dialecticId, request, User.FindFirstValue(ClaimTypes.NameIdentifier)!, thesisId)));
}
