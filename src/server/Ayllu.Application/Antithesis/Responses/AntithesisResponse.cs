using Mediator.Net.Contracts;

namespace Ayllu.Application.Antithesis.Responses;

public sealed record AntithesisResponse(
    string Id,
    string Content,
    string AuthorId,
    DateTimeOffset CreatedAt
) : IResponse
{
    public static Func<Domain.Entities.Dialectics.Antithesis, AntithesisResponse> Projection
        => a
        => new AntithesisResponse(a.Id, a.Content, a.AuthorUserId, a.CreatedAt);

    public static AntithesisResponse FromEntity(Domain.Entities.Dialectics.Antithesis antithesis) => new(
        antithesis.Id,
        antithesis.Content,
        antithesis.AuthorUserId,
        antithesis.CreatedAt
    );
}
