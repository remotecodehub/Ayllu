using Ayllu.Web.Application.Common.Abstractions.Health;
using Ayllu.Web.Application.Health.Queries;
using Ayllu.Web.Application.Health.Responses;
using MediatR;

namespace Ayllu.Web.Application.Health.Handlers;

public sealed class GetDbDiskUsageQueryHandler(IHealthService service) : IRequestHandler<GetDbDiskUsageQuery, HealthCheckResponse>
{
    public async Task<HealthCheckResponse> Handle(GetDbDiskUsageQuery request, CancellationToken cancellationToken)
        => HealthCheckResponse.FromHealthReport(await service.GetDbDiskUsageAsync(cancellationToken));
}
