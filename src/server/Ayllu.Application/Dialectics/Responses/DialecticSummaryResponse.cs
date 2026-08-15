using Ayllu.Application.Thesis.Responses;
using Ayllu.Domain.Entities.Dialectics;
using System.Linq.Expressions;

namespace Ayllu.Application.Dialectics.Responses;

public sealed record DialecticSummaryResponse(
    DialecticResponse Dialectic,
    ThesisResponse Thesis,
    int AntithesisCount,
    int SynthesisCount
) : IResponse
{
    public static Expression<Func<Dialectic, DialecticSummaryResponse>> Projection
        => d => new DialecticSummaryResponse(
            new DialecticResponse(
                d.Id,
                d.Title,
                d.Description ?? string.Empty,
                d.Status.ToString(),
                d.IsPublic,
                d.CreatedAt
            ),
            d.Thesis == null
                ? default!
                : new ThesisResponse(
                    d.Thesis.Id,
                    d.Thesis.AuthorUserId,
                    d.Thesis.Content,
                    d.Thesis.CreatedAt
                ),
            d.Antitheses.Count,
            d.Syntheses.Count
        );
}


public sealed record DialecticSummaryListResponse(IReadOnlyCollection<DialecticSummaryResponse> DialecticSummaries) : IResponse
{
    public static Expression<Func<IReadOnlyCollection<Dialectic>, DialecticSummaryListResponse>> Projection
        => dialectics => new DialecticSummaryListResponse(
            dialectics.Select(DialecticSummaryResponse.Projection.Compile()).ToList()
        );
}
