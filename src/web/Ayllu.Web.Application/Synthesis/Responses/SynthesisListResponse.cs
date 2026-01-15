using Ayllu.Web.Domain.Entities.Dialectics;
using System.Linq.Expressions;

namespace Ayllu.Web.Application.Synthesis.Responses;

public sealed record SynthesisListResponse(
    IReadOnlyList<SynthesisResponse> Items
)
{
    public static SynthesisListResponse FromSynthesesList(IList<SynthesisResponse> value) => new([.. value]);
    public static Func<IList<SynthesisResponse>, SynthesisListResponse> Projection => value => new([.. value]);
}