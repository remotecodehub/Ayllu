namespace Ayllu.Application.Synthesis.Responses;

public sealed record SynthesisResponse(
    string Id,
    string AuthorId,
    string AntithesisId,
    string Content,
    DateTimeOffset CreatedAt,
    string DialecticId,
    string ThesisId
) : IResponse
{
    public static Func<Domain.Entities.Dialectics.Synthesis, SynthesisResponse> Projection
        => s
        => new SynthesisResponse(s.Id, s.AuthorUserId, s.AntithesisId, s.Content, s.CreatedAt, s.DialecticId, s.ThesisId);

    public static SynthesisResponse FromEntity(Domain.Entities.Dialectics.Synthesis s)
            => new(s.Id, s.AuthorUserId, s.AntithesisId, s.Content, s.CreatedAt, s.DialecticId, s.ThesisId);

}