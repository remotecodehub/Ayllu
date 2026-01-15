using Ayllu.Web.Application.Common.Abstractions.Health;
using Ayllu.Web.Application.Health.Queries;
using Ayllu.Web.Application.Health.Responses;
using MediatR;

namespace Ayllu.Web.Application.Health.Handlers;

public sealed class GetHostInfoQueryHandler(IHealthService service) : IRequestHandler<GetHostInfoQuery, HealthCheckResponse>
{
    public async Task<HealthCheckResponse> Handle(GetHostInfoQuery request, CancellationToken cancellationToken)
        => HealthCheckResponse.FromHealthReport(await service.GetHostInfoAsync(cancellationToken));

}
