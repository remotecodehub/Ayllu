using Ayllu.Application.Common.Abstractions.Health;
using Ayllu.Application.Health.Queries;
using Ayllu.Application.Health.Responses;
using MediatR;

namespace Ayllu.Application.Health.Handlers;

public class GetApiDiskUsageQueryHandler(IHealthService service) : IRequestHandler<GetApiDiskUsageQuery, HealthCheckResponse>
{
    public async Task<HealthCheckResponse> Handle(GetApiDiskUsageQuery request, CancellationToken cancellationToken)
        => HealthCheckResponse.FromHealthReport(await service.GetApiDiskUsageAsync(cancellationToken));
}
