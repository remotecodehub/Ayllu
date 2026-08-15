using Ayllu.Application.Common.Abstractions.Synthesis;
using Ayllu.Application.Synthesis.Commands;
using Ayllu.Application.Synthesis.Responses;
using Mediator.Net.Context;
using Mediator.Net.Contracts;

namespace Ayllu.Application.Synthesis.Handlers;

public sealed class CreateSynthesisCommandHandler(ISynthesisService ss) : IRequestHandler<CreateSynthesisCommand, SynthesisResponse>
{
    public async Task<SynthesisResponse> Handle(IReceiveContext<CreateSynthesisCommand> context, CancellationToken cancellationToken)
        => await ss.CreateSynthesisAsync(context.Message.UserId, context.Message.DialecticId, context.Message.Request.Content, cancellationToken);
}
