using Ayllu.Web.Application.Common.Abstractions.Synthesis;
using Ayllu.Web.Application.Synthesis.Commands;
using Ayllu.Web.Application.Synthesis.Responses;
using MediatR;

namespace Ayllu.Web.Application.Synthesis.Handlers;

public sealed class CreateSynthesisCommandHandler(ISynthesisService ss) : IRequestHandler<CreateSynthesisCommand, SynthesisResponse>
{
    public async Task<SynthesisResponse> Handle(CreateSynthesisCommand request, CancellationToken cancellationToken)
        => await ss.CreateSynthesisAsync(request.UserId, request.DialecticId, request.Request.Content, cancellationToken);
}
