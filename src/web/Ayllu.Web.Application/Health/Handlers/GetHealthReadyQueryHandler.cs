using Ayllu.Web.Application.Common.Abstractions.Health;
using Ayllu.Web.Application.Health.Queries;
using Ayllu.Web.Application.Health.Responses;
using MediatR;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Ayllu.Web.Application.Health.Handlers;

public sealed class GetHealthReadyQueryHandler(IHealthService service) : IRequestHandler<GetHealthReadyQuery, HealthCheckResponse>
{
    public async Task<HealthCheckResponse> Handle(GetHealthReadyQuery request, CancellationToken cancellationToken)
        => HealthCheckResponse.FromHealthReport(await service.PerformDbReadinessCheckAsync(cancellationToken));
}
