using Ayllu.Application.Synthesis.Responses;
using MediatR;

namespace Ayllu.Application.Synthesis.Queries;

public sealed record GetSynthesesByDialecticQuery(
    string DialecticId,
    string UserId
) : IRequest<SynthesisListResponse>;