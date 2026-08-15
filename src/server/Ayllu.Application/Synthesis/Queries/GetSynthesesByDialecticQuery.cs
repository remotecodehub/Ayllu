using Ayllu.Application.Synthesis.Responses;
using Mediator.Net.Contracts;

namespace Ayllu.Application.Synthesis.Queries;

public sealed record GetSynthesesByDialecticQuery(
    string DialecticId,
    string UserId
) : IRequest;
