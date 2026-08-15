namespace Ayllu.Application.Antithesis.Responses;

public sealed record AntithesisListResponse(
    IReadOnlyList<AntithesisResponse> Items
) : IResponse
{
    public static AntithesisListResponse FromAntithesesList(ICollection<AntithesisResponse> items) => new([.. items]);
    
}