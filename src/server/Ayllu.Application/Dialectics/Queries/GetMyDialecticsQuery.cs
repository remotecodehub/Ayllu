using Ayllu.Application.Dialectics.Responses;
using MediatR;

namespace Ayllu.Application.Dialectics.Queries;

public sealed record GetMyDialecticsQuery(
    string UserId
) : IRequest<IReadOnlyList<DialecticSummaryResponse>>;
