using Ayllu.Application.Common.Abstractions.Synthesis;
using Ayllu.Application.Synthesis.Commands;
using Ayllu.Application.Synthesis.Responses;
using MediatR;

namespace Ayllu.Application.Synthesis.Handlers;

public sealed class CreateSynthesisCommandHandler(ISynthesisService ss) : IRequestHandler<CreateSynthesisCommand, SynthesisResponse>
{
    public async Task<SynthesisResponse> Handle(CreateSynthesisCommand request, CancellationToken cancellationToken)
        => await ss.CreateSynthesisAsync(request.UserId, request.DialecticId, request.Request.Content, cancellationToken);
}
