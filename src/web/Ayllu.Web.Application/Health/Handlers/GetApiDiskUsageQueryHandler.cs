using Ayllu.Web.Application.Common.Abstractions.Health;
using Ayllu.Web.Application.Health.Queries;
using Ayllu.Web.Application.Health.Responses;
using MediatR;

namespace Ayllu.Web.Application.Health.Handlers;

public class GetApiDiskUsageQueryHandler(IHealthService service) : IRequestHandler<GetApiDiskUsageQuery, HealthCheckResponse>
{
    public async Task<HealthCheckResponse> Handle(GetApiDiskUsageQuery request, CancellationToken cancellationToken)
        => HealthCheckResponse.FromHealthReport(await service.GetApiDiskUsageAsync(cancellationToken));
}
