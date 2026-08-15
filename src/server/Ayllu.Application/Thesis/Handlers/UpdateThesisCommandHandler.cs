using Ayllu.Application.Common.Abstractions.Thesis;
using Ayllu.Application.Thesis.Commands;
using Ayllu.Application.Thesis.Responses;
using Mediator.Net.Context;
using Mediator.Net.Contracts;

namespace Ayllu.Application.Thesis.Handlers;

public sealed class UpdateThesisCommandHandler(IThesisService ts)
    : IRequestHandler<UpdateThesisCommand, ThesisResponse>
{
    public async Task<ThesisResponse> Handle(IReceiveContext<UpdateThesisCommand> context, CancellationToken cancellationToken)
        => await ts.UpdateThesisAsync(context.Message.Request.Content, context.Message.UserId, context.Message.DialecticId, context.Message.ThesisId, cancellationToken);
}
