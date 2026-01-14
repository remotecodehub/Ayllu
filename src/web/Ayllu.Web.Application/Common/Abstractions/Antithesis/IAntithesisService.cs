using Ayllu.Web.Application.Antithesis.Responses;

namespace Ayllu.Web.Application.Common.Abstractions.Antithesis;

public interface IAntithesisService
{
    Task<AntithesisResponse> CreateAntithesisAsync(string userId, string dialecticId, string content, CancellationToken cancellationToken);
    Task<AntithesisListResponse> GetAntithesesByDialectAsync(string userId, string dialectId, bool isPublic, CancellationToken cancellationToken);
}
