using Ayllu.Application.Common.Abstractions.Dialectic;
using Ayllu.Application.Dialectics.Queries;
using Ayllu.Application.Dialectics.Responses;
using Mediator.Net.Context;
using Mediator.Net.Contracts;

namespace Ayllu.Application.Dialectics.Handlers;

public sealed class GetMyDialecticsQueryHandler(IDialecticService ds)
    : IRequestHandler<GetMyDialecticsQuery, IReadOnlyList<DialecticSummaryResponse>>
{
    public async Task<IReadOnlyList<DialecticSummaryResponse>> Handle(IReceiveContext<GetMyDialecticsQuery> context, CancellationToken cancellationToken)
    {
        return await ds.GetMyDialecticsAsync(context.Message.UserId, cancellationToken);
    }
}
