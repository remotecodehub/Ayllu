using Ayllu.Application.Common.Results;
using Ayllu.Application.Synthesis.Requests;
using Ayllu.Application.Synthesis.Responses;
using Ayllu.Application.Synthesis.Commands;
using Ayllu.Application.Synthesis.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ayllu.Controllers.v1;

/// <summary>
/// Processa solicitações HTTP relacionadas a sínteses dentro de uma dialética específica.
/// </summary>
/// <param name="mediator">O mediador para enviar comandos e consultas.</param>
/// <remarks>Este controlador fornece endpoints para criar e recuperar sínteses associadas a uma determinada dialética. 
/// Todas as rotas são definidas pelo identificador de dialética especificado. 
/// O controlador destina-se a ser usado como parte de uma API RESTful e segue as convenções padrão do ASP.NET Core.</remarks>
[ApiController]
[Route("api/v1/dialectics/{dialecticId}/syntheses")]
[Authorize]
[Tags("Dialectics")]
public class SynthesisController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Cria uma nova síntese associada à dialética especificada.
    /// </summary>
    /// <param name="dialecticId">Identificador unico da dialética para que a antitese será adicionada.</param>
    /// <param name="request">Objeto da requisição da síntese a ser criada.</param>
    /// <param name="cancellationToken">Token de cancelamento da operação.</param>
    /// <returns>Uma response com o resultado da criação da síntese.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Result<SynthesisResponse>), 200)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateSynthesis(string dialecticId, [FromBody] CreateSynthesisRequest request, CancellationToken cancellationToken) 
        => Ok(await mediator.Send(new CreateSynthesisCommand(dialecticId, request, User.FindFirstValue(ClaimTypes.NameIdentifier)!), cancellationToken));

    /// <summary>
    /// Retorna uma lista de sínteses associadas à dialética especificada e o usuário autenticado.
    /// </summary>
    /// <param name="dialecticId">Identificador unico da dialética para a qual as sínteses serão retornadas.</param>
    /// <param name="cancellationToken">Token de cancelamento da operação.</param>
    /// <returns>Uma response com o resultado da listagem das sínteses.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(Result<SynthesisListResponse>), 200)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> List(string dialecticId, CancellationToken cancellationToken) 
        => Ok(await mediator.Send(new GetSynthesesByDialecticQuery(dialecticId, User.FindFirstValue(ClaimTypes.NameIdentifier)!), cancellationToken));
}
