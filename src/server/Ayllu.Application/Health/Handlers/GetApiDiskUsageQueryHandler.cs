using Ayllu.Application.Common.Abstractions.Health;
using Ayllu.Application.Health.Queries;
using Ayllu.Application.Health.Responses;
using Mediator.Net.Context;
using Mediator.Net.Contracts;

namespace Ayllu.Application.Health.Handlers;

public class GetApiDiskUsageQueryHandler(IHealthService service) : IRequestHandler<GetApiDiskUsageQuery, HealthCheckResponse>
{
    public async Task<HealthCheckResponse> Handle(IReceiveContext<GetApiDiskUsageQuery> context, CancellationToken cancellationToken)
        => HealthCheckResponse.FromHealthReport(await service.GetApiDiskUsageAsync(cancellationToken));
}
