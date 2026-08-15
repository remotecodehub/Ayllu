using Ayllu.Application.Health.Queries;
using Ayllu.Application.Health.Responses;
using Mediator.Net;
using Microsoft.AspNetCore.Mvc;

namespace Ayllu.Controllers.v1;

/// <summary>
/// Controller for health check endpoints.
/// Provides information about the application's health status, disk usage, database connectivity, and host information.
/// </summary>
/// <param name="mediator"></param>
[Route("api/v1/healthcheck")]
[ApiController]
[Tags("HealthChecks")]
public class HealthController(IMediator mediator) : ControllerBase
{
    [HttpGet("api")]
    public async Task<IActionResult> Check(CancellationToken cancellationToken)
        => Ok(await mediator.RequestAsync<GetHealthCheckQuery, HealthCheckResponse>(new GetHealthCheckQuery(), cancellationToken));

    [HttpGet("api/disk")]
    public async Task<IActionResult> DiskApi(CancellationToken cancellationToken)
        => Ok(await mediator.RequestAsync<GetApiDiskUsageQuery, HealthCheckResponse>(new GetApiDiskUsageQuery(), cancellationToken));

    [HttpGet("db")]
    public async Task<IActionResult> GetHealthReadyCheck(CancellationToken cancellationToken)
        => Ok(await mediator.RequestAsync<GetHealthReadyQuery, HealthCheckResponse>(new GetHealthReadyQuery(), cancellationToken));

    [HttpGet("db/disk")]
    public async Task<IActionResult> DiskDb(CancellationToken cancellationToken)
        => Ok(await mediator.RequestAsync<GetDbDiskUsageQuery, HealthCheckResponse>(new GetDbDiskUsageQuery(), cancellationToken));

    [HttpGet("host")]
    public async Task<IActionResult> GetHostInfo(CancellationToken cancellationToken)
        => Ok(await mediator.RequestAsync<GetHostInfoQuery, HealthCheckResponse>(new GetHostInfoQuery(), cancellationToken));
}
