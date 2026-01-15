using Ayllu.Web.Application.Health.Responses;
using MediatR;

namespace Ayllu.Web.Application.Health.Queries;

public sealed record GetApiDiskUsageQuery() : IRequest<HealthCheckResponse>;
