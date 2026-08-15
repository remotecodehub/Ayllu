using Ayllu.Application.Common.Abstractions.Dialectic;
using Ayllu.Application.Dialectics.Commands;
using Ayllu.Application.Dialectics.Responses;
using Mediator.Net.Context;
using Mediator.Net.Contracts;

namespace Ayllu.Application.Dialectics.Handlers;

public sealed class CreateDialecticCommandHandler(IDialecticService ds)
    : IRequestHandler<CreateDialecticCommand, CreateDialecticResponse>
{
    public async Task<CreateDialecticResponse> Handle(IReceiveContext<CreateDialecticCommand> context, CancellationToken cancellationToken)
    {
        var dialectic = await ds.CreateDialecticAsync(context.Message.Request.Title, context.Message.Request.Description, context.Message.Request.IsPublic, context.Message.UserId, cancellationToken);

        return new CreateDialecticResponse(dialectic.DialecticId);
    }
}