using Ayllu.Application.Common.Abstractions.Health;
using Ayllu.Application.Health.Queries;
using Ayllu.Application.Health.Responses;
using MediatR;

namespace Ayllu.Application.Health.Handlers;

public sealed class GetDbDiskUsageQueryHandler(IHealthService service) : IRequestHandler<GetDbDiskUsageQuery, HealthCheckResponse>
{
    public async Task<HealthCheckResponse> Handle(GetDbDiskUsageQuery request, CancellationToken cancellationToken)
        => HealthCheckResponse.FromHealthReport(await service.GetDbDiskUsageAsync(cancellationToken));
}
