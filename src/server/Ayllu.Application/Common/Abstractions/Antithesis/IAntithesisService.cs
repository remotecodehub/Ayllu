using Ayllu.Application.Antithesis.Responses;

namespace Ayllu.Application.Common.Abstractions.Antithesis;

public interface IAntithesisService
{
    Task<AntithesisResponse> CreateAntithesisAsync(string userId, string dialecticId, string content, CancellationToken cancellationToken);
    Task<AntithesisListResponse> GetAntithesesByDialectAsync(string userId, string dialectId, bool isPublic, CancellationToken cancellationToken);
}
