using Ayllu.Web.Application.Common.Results;
using Ayllu.Web.Application.Dialectics.Commands;
using Ayllu.Web.Application.Dialectics.Queries;
using Ayllu.Web.Application.Dialectics.Requests;
using Ayllu.Web.Application.Dialectics.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ayllu.Web.Controllers.v1;

/// <summary>
/// Controller for managing dialectics.
/// </summary>
[ApiController]
[Route("api/v1/dialectics")]
[Tags("Dialectics")]
[Authorize]
public class DialecticsController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Cria uma nova dialética
    /// </summary>
    /// <param name="request">Objeto da requisição enviado pelo cliente para criar a nova dialética.</param>
    /// <param name="cancellationToken">Token de cancelamento da operação.</param>
    /// <returns>Uma response 200 OK com o resultado da criação da dialética.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Result<CreateDialecticResponse>), 200)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateDialectic([FromBody] CreateDialecticRequest request, CancellationToken cancellationToken) 
        => Ok(await mediator.Send(new CreateDialecticCommand(request, User.FindFirstValue(ClaimTypes.NameIdentifier)!), cancellationToken));

    /// <summary>
    /// Fecha a dialética especificada, marcando a como não ativa.
    /// </summary>
    /// <param name="id">O identificador unico da dialética a ser fechada.</param>
    /// <param name="cancellationToken">Token de cancelamento da operação.</param>
    /// <returns>Uma response com HTTP status code 200 (Ok) se a operação for bem sucedida.</returns>
    [HttpPost("{id}/close")]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status500InternalServerError)] 
    public async Task<IActionResult> CloseDialectic(string id, CancellationToken cancellationToken) 
        => Ok(await mediator.Send(new CloseDialecticCommand(id, User.FindFirstValue(ClaimTypes.NameIdentifier)!), cancellationToken));

    /// <summary>
    /// Busca uma dialética associada ao usuario com base no identificador único.
    /// </summary>
    /// <param name="id">O identificador único da dialética a ser retornada.</param>
    /// <param name="cancellationToken">Token de cancelamento da operação.</param>
    /// <returns>Uma response com HTTP status code 200 OK que representa o resultado da operação.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken) 
        => Ok(await mediator.Send(new GetDialecticByIdQuery(id, User.FindFirstValue(ClaimTypes.NameIdentifier)!), cancellationToken));

    /// <summary>
    /// Retorna as dialéticas do usuário atual.
    /// </summary>
    /// <param name="cancellationToken">Token de cancelamento da operação.</param>
    /// <returns>Uma response com HTTP status code 200 OK que contém o resultado da operação.</returns>
    [HttpGet("mine")]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status500InternalServerError)] 
    public async Task<IActionResult> GetMyDialectics(CancellationToken cancellationToken) 
        => Ok(await mediator.Send(new GetMyDialecticsQuery(User.FindFirstValue(ClaimTypes.NameIdentifier)!), cancellationToken));

    /// <summary>
    /// Recupera a dialética dos amigos do usuário.
    /// </summary>
    /// <param name="cancellationToken">Token de cancelamento da operação.</param>
    /// <returns>Uma response com HTTP status code 200 OK que representa o resultado da operação.</returns>
    [HttpGet("friends")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetFriendsDialectics(CancellationToken cancellationToken) 
        => Ok(await mediator.Send(new GetFriendsDialecticsQuery(User.FindFirstValue(ClaimTypes.NameIdentifier)!), cancellationToken));
}
