using Ayllu.Application.Common.Abstractions.Dialectic;
using Ayllu.Application.Dialectics.Commands;
using Ayllu.Application.Dialectics.Responses;
using Mediator.Net.Context;
using Mediator.Net.Contracts;

namespace Ayllu.Application.Dialectics.Handlers;

public sealed class CloseDialecticCommandHandler(IDialecticService ds) : IRequestHandler<CloseDialecticCommand, DialecticResponse?>
{
    public async Task<DialecticResponse?> Handle(IReceiveContext<CloseDialecticCommand> context, CancellationToken cancellationToken)
    {
        return await ds.CloseDialecticAsync(context.Message.DialecticId, context.Message.UserId, cancellationToken);
    }
}
