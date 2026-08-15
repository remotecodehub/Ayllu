using Ayllu.Application.Dialectics.Responses;
using Mediator.Net.Contracts;

namespace Ayllu.Application.Dialectics.Queries;

public sealed record GetDialecticByIdQuery(
    string DialecticId,
    string UserId
) : IRequest;
