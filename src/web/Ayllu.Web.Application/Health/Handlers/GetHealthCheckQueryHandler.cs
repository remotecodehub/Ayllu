using Ayllu.Web.Application.Common.Abstractions.Health;
using Ayllu.Web.Application.Health.Queries;
using Ayllu.Web.Application.Health.Responses;
using MediatR;

namespace Ayllu.Web.Application.Health.Handlers;

public sealed class GetHealthCheckQueryHandler(IHealthService service) : IRequestHandler<GetHealthCheckQuery, HealthCheckResponse>
{
    public async Task<HealthCheckResponse> Handle(GetHealthCheckQuery request, CancellationToken cancellationToken)
        => HealthCheckResponse.FromHealthReport(await service.GetHealthCheckAsync(cancellationToken));
}
