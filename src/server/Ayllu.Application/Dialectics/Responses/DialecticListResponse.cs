namespace Ayllu.Application.Dialectics.Responses;

public sealed record DialecticListResponse(
    IReadOnlyList<DialecticResponse> Items
)
{
    public static DialecticListResponse FromEntityList(IList<DialecticResponse> dialectics) => new([.. dialectics]);
    public static Func<ICollection<DialecticResponse>, DialecticListResponse> Projection 
        => dialectics
        => new([.. dialectics]);
}