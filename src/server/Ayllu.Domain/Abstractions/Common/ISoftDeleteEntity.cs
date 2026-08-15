namespace Ayllu.Domain.Abstractions.Common;

public interface ISoftDeleteEntity
{
    bool IsDeleted { get; }
    DateTimeOffset? DeletedAt { get; set; }
}
