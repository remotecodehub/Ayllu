using Ayllu.Application.Synthesis.Responses;

namespace Ayllu.Application.Common.Abstractions.Synthesis;

public interface ISynthesisService
{
    Task<SynthesisResponse> CreateSynthesisAsync(string userId, string dialecticId, string content, CancellationToken cancellationToken);
    Task<SynthesisListResponse> GetSynthesesByDialecticAsync(string userId, string dialecticId, CancellationToken cancellationToken);
}
