using Ayllu.Application.Synthesis.Requests;
using Ayllu.Application.Synthesis.Responses;
using MediatR;

namespace Ayllu.Application.Synthesis.Commands;

public sealed record CreateSynthesisCommand(
    string DialecticId,
    CreateSynthesisRequest Request,
    string UserId
) : IRequest<SynthesisResponse>;