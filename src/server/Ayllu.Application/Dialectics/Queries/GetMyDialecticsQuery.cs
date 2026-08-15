using Ayllu.Application.Dialectics.Responses;
using Mediator.Net.Contracts;

namespace Ayllu.Application.Dialectics.Queries;

public sealed record GetMyDialecticsQuery(
    string UserId
) : IRequest;
