using Ayllu.Application.Health.Responses;
using MediatR;

namespace Ayllu.Application.Health.Queries;

public sealed record GetHealthReadyQuery() : IRequest<HealthCheckResponse>;

