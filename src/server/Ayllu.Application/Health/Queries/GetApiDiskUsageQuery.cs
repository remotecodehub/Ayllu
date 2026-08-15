using Ayllu.Application.Health.Responses;
using MediatR;

namespace Ayllu.Application.Health.Queries;

public sealed record GetApiDiskUsageQuery() : IRequest<HealthCheckResponse>;
