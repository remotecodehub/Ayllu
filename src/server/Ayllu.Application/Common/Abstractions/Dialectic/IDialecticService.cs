using Ayllu.Application.Dialectics.Responses;

namespace Ayllu.Application.Common.Abstractions.Dialectic;

public interface IDialecticService
{
    Task<DialecticResponse> CreateDialecticAsync(string title, string description, bool isPublic, string userId, CancellationToken cancellationToken);
    Task<DialecticResponse> CloseDialecticAsync(string id, string userId, CancellationToken cancellationToken);
    Task<IReadOnlyList<DialecticSummaryResponse>> GetMyDialecticsAsync(string userId, CancellationToken cancellationToken);
    Task<DialecticResponse> GetDialecticByIdAsync(string id, CancellationToken cancellationToken);
    Task<DialecticListResponse> GetDialecticsListAsync(CancellationToken cancellationToken);
}
