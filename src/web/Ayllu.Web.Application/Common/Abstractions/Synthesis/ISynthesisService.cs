using Ayllu.Web.Application.Synthesis.Responses;

namespace Ayllu.Web.Application.Common.Abstractions.Synthesis;

public interface ISynthesisService
{
    Task<SynthesisResponse> CreateSynthesisAsync(string userId, string dialecticId, string content, CancellationToken cancellationToken);
    Task<SynthesisListResponse> GetSynthesesByDialecticAsync(string userId, string dialecticId, CancellationToken cancellationToken);
}
