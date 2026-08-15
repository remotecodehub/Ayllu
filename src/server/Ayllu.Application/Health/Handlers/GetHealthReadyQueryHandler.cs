using Ayllu.Application.Common.Abstractions.Health;
using Ayllu.Application.Health.Queries;
using Ayllu.Application.Health.Responses;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Ayllu.Application.Health.Handlers;

public sealed class GetHealthReadyQueryHandler(IHealthService service) : IRequestHandler<GetHealthReadyQuery, HealthCheckResponse>
{
    public async Task<HealthCheckResponse> Handle(IReceiveContext<GetHealthReadyQuery> context, CancellationToken cancellationToken)
        => HealthCheckResponse.FromHealthReport(await service.PerformDbReadinessCheckAsync(cancellationToken));
}
