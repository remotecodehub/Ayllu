namespace Ayllu.Application.Thesis.Responses;

public sealed record ThesisResponse(
    string Id,
    string AuthorId,
    string Content,
    DateTimeOffset CreatedAt
) : IResponse
{
    public static ThesisResponse FromEntity(Domain.Entities.Dialectics.Thesis result) => new(result.Id, result.AuthorUserId, result.Content, result.CreatedAt);
}