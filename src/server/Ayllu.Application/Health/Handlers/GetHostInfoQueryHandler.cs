using Ayllu.Application.Common.Abstractions.Health;
using Ayllu.Application.Health.Queries;
using Ayllu.Application.Health.Responses;
using Mediator.Net.Context;
using Mediator.Net.Contracts;

namespace Ayllu.Application.Health.Handlers;

public sealed class GetHostInfoQueryHandler(IHealthService service) : IRequestHandler<GetHostInfoQuery, HealthCheckResponse>
{
    public async Task<HealthCheckResponse> Handle(IReceiveContext<GetHostInfoQuery> context, CancellationToken cancellationToken)
        => HealthCheckResponse.FromHealthReport(await service.GetHostInfoAsync(cancellationToken));
}
