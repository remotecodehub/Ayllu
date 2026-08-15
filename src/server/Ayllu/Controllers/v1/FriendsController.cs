using Ayllu.Application.Common.Results;
using Ayllu.Application.Identity.Requests;
using Ayllu.Application.Identity.Responses;
using Ayllu.Application.Identity.Commands;
using Ayllu.Application.Identity.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace Ayllu.Controllers.v1;

/// <summary>
/// Controller de autenticação para extender endpoints mapeados pelo Identity.
/// </summary>
/// <param name="mediator">O mediador para enviar comandos e consultas.</param>
[Route("api/v1/identity/me/friends")]
[ApiController]
[Tags("Identity")]
public class FriendsController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Busca usuario atual.
    /// </summary>
    /// <remarks>Este endpoint requer que o usuário esteja autenticado. A resposta inclui detalhes do usuário
    /// associados ao contexto de autenticação atual</remarks>
    /// <param name="request">Objeto da requisição enviado no corpo.</param>
    /// <param name="cancellationToken">Token de cancelamento da operação.</param>
    /// <returns>Uma response <see cref="UserFriendInviteSentResponse"/> com HTTP status code 200 OK que contém as informações do usuário autenticado.</returns>
    [HttpPost()]
    [Authorize]
    [ProducesResponseType(typeof(Result<UserFriendInviteSentResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddFriendAsync([FromBody] SendUserFriendInviteRequest request, CancellationToken cancellationToken)
        => Ok(Result<UserFriendInviteSentResponse>.Success(await mediator.Send(new SendUserFriendInviteCommand(request, User.FindFirstValue(ClaimTypes.NameIdentifier)!), cancellationToken)));
    
    /// <summary>
    /// Retorna lista de usuários com vinculo de amizade com o usuario atual
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(Result<ICollection<ApplicationUserFriendResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ListFriendsAsync(CancellationToken cancellationToken)
        => Ok(Result<ICollection<ApplicationUserFriendResponse>>.Success(await mediator.Send(new GetUserFriendsQuery(User.FindFirstValue(ClaimTypes.NameIdentifier)!), cancellationToken)));

}