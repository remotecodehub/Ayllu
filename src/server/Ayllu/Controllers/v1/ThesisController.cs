using Ayllu.Application.Common.Results;
using Ayllu.Application.Thesis.Requests;
using Ayllu.Application.Thesis.Responses;
using Ayllu.Application.Synthesis.Responses;
using Ayllu.Application.Thesis.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ayllu.Controllers.v1;

/// <summary>
/// Handles HTTP requests related to thesis resources within a specific dialectic context.
/// </summary>
/// <remarks>This controller provides endpoints for creating and updating thesis entities associated with a given
/// dialectic. All routes are prefixed with 'api/v1/dialectics/{dialecticId}/thesis'.</remarks>
[ApiController]
[Route("api/v1/dialectics/{dialecticId}/thesis")]
[Tags("Dialectics")]
[Authorize]
public class ThesisController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Publica uma tese associada à dialética especificada.
    /// </summary>
    /// <param name="request">O objeto da requisição para a criação e publicação da tese associada à dialética. Não pode ser nulo ou vazio.</param>
    /// <param name="dialecticId">O identificador único da dialética cuja tese será publicada. Não pode ser nulo ou vazio.</param>
    /// <param name="cancellationToken">Token de cancelamento da operação.</param>
    /// <returns>Uma response indicando o resultado da operação com HTTP status code 200 OK.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Result<ThesisResponse>), 200)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PublishThesis([FromBody]CreateThesisRequest request, string dialecticId, CancellationToken cancellationToken) 
        => Ok(await mediator.Send(new CreateThesisCommand(dialecticId, request, User.FindFirstValue(ClaimTypes.NameIdentifier)!), cancellationToken));

    /// <summary>
    /// Atualiza uma tese associada à uma dialética específica.
    /// </summary>
    /// <param name="request">O objeto da requisição para a atualização da tese associada à dialética. Não pode ser nulo ou vazio.</param>
    /// <param name="dialecticId">O identificador único da dialética cuja tese será atualizada. Não pode ser nulo ou vazio.</param>
    /// <param name="thesisId">O identificador único da tese a ser atualizada.</param>
    /// <param name="cancellationToken">Token de cancelamento da operação.</param>    
    /// <returns>Uma response HTTP 200 Ok com o resultado da atualização.</returns>
    [HttpPut("{thesisId}")]
    [ProducesResponseType(typeof(Result<ThesisResponse>), 200)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateThesis([FromBody] UpdateThesisRequest request, string dialecticId, string thesisId, CancellationToken cancellationToken) 
        => Ok(await mediator.Send(new UpdateThesisCommand(dialecticId, request, User.FindFirstValue(ClaimTypes.NameIdentifier)!, thesisId), cancellationToken));
    
}
