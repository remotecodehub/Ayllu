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
[Route("api/v1/identity")]
[ApiController]
[Tags("Identity")]
public class IdentityController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Busca usuario atual.
    /// </summary>
    /// <remarks>Este endpoint requer que o usuário esteja autenticado. A resposta inclui detalhes do usuário
    /// associados ao contexto de autenticação atual</remarks>
    /// <param name="cancellationToken">Token de cancelamento da operação.</param>
    /// <returns>Uma response <see cref="ApplicationUserResponse"/> com HTTP status code 200 OK que contém as informações do usuário autenticado.</returns>
    [HttpGet("me")]
    [Authorize]
    [ProducesResponseType(typeof(Result<ApplicationUserResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCurrentUserAsync(CancellationToken cancellationToken)
        => Ok(Result<ApplicationUserResponse>.Success(await mediator.Send(new GetCurrentUserQuery(User.FindFirstValue(ClaimTypes.NameIdentifier)!), cancellationToken)));
    /// <summary>
    /// Atualiza o perfil
    /// </summary>
    /// <remarks>Este endpoint requer que o usuário esteja autenticado. A resposta retorna os detalhes atualizados do usuário, ou uma response apropriada com o erro se a requisição for inválida ou falhar.</remarks>
    /// <param name="request">Objeto da requisição com os dados a serem atualizados. Não pode ser nulo.</param>
    /// <param name="cancellationToken">Token de cancelamento da operação.</param>
    /// <returns>Um <see cref="IActionResult"/> contendo um <see cref="Result{T}"/> com o perfil atualizado no sucesso, ou
    /// uma response de erro se a atualização falhar.</returns>
    [HttpPost("me")]
    [Authorize]
    [ProducesResponseType(typeof(Result<ApplicationUserResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateCurrentUserAsync([FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
        => Ok(Result<ApplicationUserResponse>.Success(await mediator.Send(new UpdateUserCommand(request, User.FindFirstValue(ClaimTypes.NameIdentifier)!), cancellationToken)));
    
    /// <summary>
    /// Realiza logout.
    /// </summary>
    /// <remarks>Este endpoint requer que o usuário esteja autenticado. A resposta inclui detalhes do 
    /// resultado da operação efetuada se bem sucedida</remarks>
    /// <param name="cancellationToken">Token de cancelamento da operação.</param>
    /// <returns>Uma response <see cref="bool"/> com HTTP status code 200 OK que informa se o logout foi efetuado.</returns>
    [HttpGet("logout")]
    [Authorize]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> LogoutUserAsync(CancellationToken cancellationToken)
        => Ok(Result<bool>.Success(await mediator.Send(new LogoutQuery(User.FindFirstValue(ClaimTypes.NameIdentifier)!), cancellationToken)));


}