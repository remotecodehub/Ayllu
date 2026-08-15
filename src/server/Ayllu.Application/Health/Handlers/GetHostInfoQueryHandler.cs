using Ayllu.Application.Common.Abstractions.Health;
using Ayllu.Application.Health.Queries;
using Ayllu.Application.Health.Responses;
using MediatR;

namespace Ayllu.Application.Health.Handlers;

public sealed class GetHostInfoQueryHandler(IHealthService service) : IRequestHandler<GetHostInfoQuery, HealthCheckResponse>
{
    public async Task<HealthCheckResponse> Handle(GetHostInfoQuery request, CancellationToken cancellationToken)
        => HealthCheckResponse.FromHealthReport(await service.GetHostInfoAsync(cancellationToken));

}
