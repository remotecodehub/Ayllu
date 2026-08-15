using Ayllu.Application.Common.Abstractions.Dialectic;
using Ayllu.Application.Dialectics.Queries;
using Ayllu.Application.Dialectics.Responses;
using MediatR;

namespace Ayllu.Application.Dialectics.Handlers;

public sealed class GetMyDialecticsQueryHandler(IDialecticService ds)
    : IRequestHandler<GetMyDialecticsQuery, IReadOnlyList<DialecticSummaryResponse>>
{
    public async Task<IReadOnlyList<DialecticSummaryResponse>> Handle(
        GetMyDialecticsQuery request,
        CancellationToken cancellationToken)
    {
        return await ds.GetMyDialecticsAsync(request.UserId, cancellationToken);
    }
}
