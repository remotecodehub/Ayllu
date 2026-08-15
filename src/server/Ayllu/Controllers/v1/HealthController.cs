using Ayllu.Application.Common.Results;
using Ayllu.Application.Health.Responses;
using Ayllu.Application.Health.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Scalar.AspNetCore;
using System.Threading.Tasks;

namespace Ayllu.Controllers.v1;

/// <summary>
/// Defines an API controller that provides endpoints for health checks of the application.
/// </summary>
/// <param name="mediator">The mediator instance to send command's and query's to the application layer</param>
/// <remarks>Use this controller to verify that the application is running and responsive. Health check endpoints
/// are typically used by monitoring systems or load balancers to determine the application's availability.</remarks>
[Route("api/v1/healthcheck")]
[ApiController]
[Tags("HealthChecks")] 
public class HealthController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Executa um check na health da API
    /// </summary>
    /// <param name="cancellationToken">Um token de cancelamento que pode ser usado para cancelar a operação de verificação de integridade.</param>
    /// <returns>Um <see cref="IActionResult"/> conténdo um objeto <see cref="HealthReport"/> se bem sucedido.
    /// Se não, um <see cref="ProblemDetails"/> com a descrição do erro</returns>
    [HttpGet("api")]
    [ProducesResponseType(typeof(HealthCheckResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status405MethodNotAllowed)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status412PreconditionFailed)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Check(CancellationToken cancellationToken) 
        => Ok(await mediator.Send(new GetHealthCheckQuery(), cancellationToken));

    /// <summary>
    /// Retorna o uso de disco para o server da API.
    /// </summary>
    /// <param name="cancellationToken">Um token de cancelamento que pode ser usado para cancelar a operação de verificação de integridade.</param>
    /// <returns>Um objeto <see cref="IActionResult"/> contendo uma response do tipo <see cref="HealthCheckResponse"/> com o uso de disco atual para a aplicação
    /// se a requisição for bem sucedida.</returns>
    [HttpGet("api/disk")]
    [ProducesResponseType(typeof(HealthCheckResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status405MethodNotAllowed)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DiskApi(CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetApiDiskUsageQuery(), cancellationToken));

    /// <summary>
    /// Executa uma verificação de prontidão e retorna o status atual de prontidão do serviço.
    /// </summary>
    /// <remarks>Este endpoint é normalmente usado por balanceadores de carga ou sistemas de orquestração para determinar se
    /// o serviço está pronto para receber tráfego. A resposta inclui informações detalhadas de prontidão ou um status de erro
    /// se o serviço não estiver pronto.</remarks>
    /// <param name="cancellationToken">Um token de cancelamento que pode ser usado para cancelar a operação de verificação de integridade.</param>
    /// <returns>Um objeto <see cref="IActionResult"/> contendo uma <see cref="Result{HealthReadyResponse}"/> com o status da leitura se bem sucedido;
    /// Se não, um resultado <see cref="ProblemDetails"/> descrevendo o erro.</returns>
    [HttpGet("db")]
    [ProducesResponseType(typeof(HealthCheckResponse), 200)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status405MethodNotAllowed)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status412PreconditionFailed)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetHealthReadyCheck(CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetHealthReadyQuery(), cancellationToken));

    /// <summary>
    /// Retorna o uso de disco para o server da Database.
    /// </summary>
    /// <param name="cancellationToken">Um token de cancelamento que pode ser usado para cancelar a operação de verificação de integridade.</param>
    /// <returns>Um objeto <see cref="IActionResult"/> contendo uma response 
    /// do tipo <see cref="HealthCheckResponse"/> com o uso de disco para a base de dados.</returns>
    [HttpGet("db/disk")]
    [ProducesResponseType(typeof(HealthCheckResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status405MethodNotAllowed)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DiskDb(CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetDbDiskUsageQuery(), cancellationToken));

    /// <summary>
    /// Retorna informações sobre o host
    /// </summary>
    /// <param name="cancellationToken">Um token de cancelamento que pode ser usado para cancelar a operação de verificação de integridade.</param>
    /// <returns>Uma response dp tipo <see cref="HealthCheckResponse"/> com informações sobre o host que a API está executando, 
    /// se não, uma response <see cref="ProblemDetails"/> descrevendo o erro</returns>
    [HttpGet("host")]
    [ProducesResponseType(typeof(HealthCheckResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status405MethodNotAllowed)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetHostInfo(CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetHostInfoQuery(), cancellationToken));

}