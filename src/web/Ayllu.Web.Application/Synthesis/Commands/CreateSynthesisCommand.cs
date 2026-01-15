using Ayllu.Web.Application.Synthesis.Requests;
using Ayllu.Web.Application.Synthesis.Responses;
using MediatR;

namespace Ayllu.Web.Application.Synthesis.Commands;

public sealed record CreateSynthesisCommand(
    string DialecticId,
    CreateSynthesisRequest Request,
    string UserId
) : IRequest<SynthesisResponse>;