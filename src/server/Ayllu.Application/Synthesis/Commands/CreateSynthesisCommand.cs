using Ayllu.Application.Synthesis.Requests;
using Ayllu.Application.Synthesis.Responses;
using Mediator.Net.Contracts;

namespace Ayllu.Application.Synthesis.Commands;

public sealed record CreateSynthesisCommand(
    string DialecticId,
    CreateSynthesisRequest Request,
    string UserId
) : IRequest<SynthesisResponse>;
