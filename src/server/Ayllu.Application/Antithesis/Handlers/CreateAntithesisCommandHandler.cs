using Ayllu.Application.Antithesis.Commands;
using Ayllu.Application.Antithesis.Responses;
using Ayllu.Application.Common.Abstractions.Antithesis;
using Mediator.Net.Context;
using Mediator.Net.Contracts;

namespace Ayllu.Application.Antithesis.Handlers;

public sealed class CreateAntithesisCommandHandler(IAntithesisService ats)
    : IRequestHandler<CreateAntithesisCommand, AntithesisResponse>
{
    public async Task<AntithesisResponse> Handle(IReceiveContext<CreateAntithesisCommand> context, CancellationToken cancellationToken)
        => await ats.CreateAntithesisAsync(context.Message.UserId, context.Message.DialecticId, context.Message.Request.Content, cancellationToken);
}
