using Ayllu.Web.Application.Health.Responses;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Ayllu.Web.Application.Common.Abstractions.Health;

public interface IHealthService
{
    Task<HealthReport> GetApiDiskUsageAsync(CancellationToken cancellationToken);
    Task<HealthReport> GetDbDiskUsageAsync(CancellationToken cancellationToken);
    Task<HealthReport> GetHealthCheckAsync(CancellationToken cancellationToken);
    Task<HealthReport> GetHostInfoAsync(CancellationToken cancellationToken);
    Task<HealthReport> PerformDbReadinessCheckAsync(CancellationToken cancellationToken);
}
