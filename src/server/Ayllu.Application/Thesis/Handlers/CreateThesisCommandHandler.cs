using Ayllu.Application.Common.Abstractions.Thesis;
using Ayllu.Application.Thesis.Commands;
using Ayllu.Application.Thesis.Responses;
using Mediator.Net.Context;
using Mediator.Net.Contracts;

namespace Ayllu.Application.Thesis.Handlers;

public sealed class CreateThesisCommandHandler(IThesisService ts)
    : IRequestHandler<CreateThesisCommand, ThesisResponse>
{
    public async Task<ThesisResponse> Handle(IReceiveContext<CreateThesisCommand> context, CancellationToken cancellationToken)
        => await ts.CreateThesisAsync(context.Message.Request.Content, context.Message.UserId, context.Message.DialecticId, cancellationToken);
}
