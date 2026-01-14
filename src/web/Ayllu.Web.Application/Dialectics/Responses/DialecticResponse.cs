using Ayllu.Web.Domain.Entities.Dialectics;
using System.Linq.Expressions;

namespace Ayllu.Web.Application.Dialectics.Responses;

public sealed record DialecticResponse(
    string DialecticId,
    string Title,
    string Description,
    string Status,
    bool IsPublic,
    DateTimeOffset CreatedAt
)
{
    public static Expression<Func<Dialectic, DialecticResponse>> Projection
        => d 
        => new DialecticResponse(
            d.Id,
            d.Title,
            d.Description ?? string.Empty,
            d.Status.ToString(),
            d.IsPublic,
            d.CreatedAt
        );

    public static DialecticResponse FromEntity(Dialectic d) => new (
            d.Id,
            d.Title,
            d.Description ?? string.Empty,
            d.Status.ToString(),
            d.IsPublic,
            d.CreatedAt
        );

}