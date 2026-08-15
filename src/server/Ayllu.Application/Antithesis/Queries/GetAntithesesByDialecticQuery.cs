using Ayllu.Application.Antithesis.Responses;
using Mediator.Net.Contracts;

namespace Ayllu.Application.Antithesis.Queries;

public sealed record GetAntithesesByDialecticQuery(
    string DialecticId,
    string UserId,
    bool PublicAntithesisOnly
) : IRequest;
