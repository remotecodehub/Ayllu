using Ayllu.Web.Application.Common.Abstractions.Dialectic;
using Ayllu.Web.Application.Dialectics.Queries;
using Ayllu.Web.Application.Dialectics.Responses;
using MediatR;

namespace Ayllu.Web.Application.Dialectics.Handlers;

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
