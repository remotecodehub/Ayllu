using Ayllu.Application.Thesis.Responses;

namespace Ayllu.Application.Common.Abstractions.Thesis;

public interface IThesisService
{
    Task<ThesisResponse> CreateThesisAsync(string content, string userId, string dialecticId, CancellationToken cancellationToken);
    Task<ThesisResponse> UpdateThesisAsync(string content, string userId, string dialecticId, string thesisId, CancellationToken cancellationToken);
}
