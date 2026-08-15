using Ayllu.Application.Common.Abstractions.Health;
using Ayllu.Application.Health.Queries;
using Ayllu.Application.Health.Responses;
using MediatR;

namespace Ayllu.Application.Health.Handlers;

public sealed class GetHealthCheckQueryHandler(IHealthService service) : IRequestHandler<GetHealthCheckQuery, HealthCheckResponse>
{
    public async Task<HealthCheckResponse> Handle(GetHealthCheckQuery request, CancellationToken cancellationToken)
        => HealthCheckResponse.FromHealthReport(await service.GetHealthCheckAsync(cancellationToken));
}
