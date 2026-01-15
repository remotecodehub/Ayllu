using Ayllu.Web.Application.Antithesis.Commands;
using Ayllu.Web.Application.Antithesis.Queries;
using Ayllu.Web.Application.Antithesis.Requests;
using Ayllu.Web.Application.Antithesis.Responses;
using Ayllu.Web.Application.Common.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ayllu.Web.Controllers.v1;

/// <summary>
/// Handles HTTP requests for managing antitheses within a specific dialectic.
/// </summary>
/// <remarks>This controller provides endpoints for creating and listing antitheses associated with a given
/// dialectic. All routes are scoped to a particular dialectic, identified by the 'dialecticId' route parameter.
/// Intended for use in RESTful API scenarios.</remarks>
[ApiController]
[Route("api/v1/dialectics/{dialecticId}/antitheses")]
[Tags("Dialectics")]
[Authorize]

public class AntithesisController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Cria uma antítese associada com a dialética especificada.
    /// </summary>
    /// <param name="dialecticId">Identificador unico da dialética para que a antitese será adicionada.</param>
    /// <param name="request">Objeto da requisição da antitese a ser criada.</param>
    /// <param name="cancellationToken">Token de cancelamento da operação.</param>
    /// <returns>Uma response com o resultado da criação da antítese.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Result<AntithesisResponse>), 200)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAntithesis(string dialecticId, [FromBody] CreateAntithesisRequest request, CancellationToken cancellationToken) 
        => Ok(await mediator.Send(new CreateAntithesisCommand(dialecticId, request, User.FindFirstValue(ClaimTypes.NameIdentifier)!),cancellationToken));

    /// <summary>
    /// Lista antíteses associadas à dialética especificada.
    /// </summary>
    /// <param name="dialecticId">Identificador unico da dialética para que a antitese será adicionada.</param>
    /// <param name="publicOnly">Parametro de query que indica busca de antiteses publicas apenas ou não.</param>
    /// <param name="cancellationToken">Token de cancelamento da operação.</param>
    /// <returns>Uma response com a lista do resultado da listagem de antíteses.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(Result<AntithesisListResponse>), 200)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> List(string dialecticId, [FromQuery(Name = "publicOnly")] bool? publicOnly, CancellationToken cancellationToken) 
        => Ok(await mediator.Send(new GetAntithesesByDialecticQuery(dialecticId, User.FindFirstValue(ClaimTypes.NameIdentifier)!, publicOnly ?? true), cancellationToken));
}
