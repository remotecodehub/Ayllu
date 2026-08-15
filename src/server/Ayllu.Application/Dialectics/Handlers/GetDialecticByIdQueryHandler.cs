using Ayllu.Application.Common.Abstractions.Dialectic;
using Ayllu.Application.Dialectics.Queries;
using Ayllu.Application.Dialectics.Responses;
using Mediator.Net.Context;
using Mediator.Net.Contracts;

namespace Ayllu.Application.Dialectics.Handlers;

public sealed class GetDialecticByIdQueryHandler(IDialecticService ds)
    : IRequestHandler<GetDialecticByIdQuery, DialecticResponse?>
{
    public async Task<DialecticResponse?> Handle(IReceiveContext<GetDialecticByIdQuery> context, CancellationToken cancellationToken)
        => await ds.GetDialecticByIdAsync(context.Message.DialecticId, cancellationToken);
}