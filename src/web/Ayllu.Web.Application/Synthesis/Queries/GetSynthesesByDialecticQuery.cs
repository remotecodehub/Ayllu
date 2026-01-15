using Ayllu.Web.Application.Synthesis.Responses;
using MediatR;

namespace Ayllu.Web.Application.Synthesis.Queries;

public sealed record GetSynthesesByDialecticQuery(
    string DialecticId,
    string UserId
) : IRequest<SynthesisListResponse>;