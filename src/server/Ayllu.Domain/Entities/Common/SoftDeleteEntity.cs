using Ayllu.Domain.Abstractions.Common;

namespace Ayllu.Domain.Entities.Common;

public class SoftDeleteEntity<TKey> : Entity<TKey>, ISoftDeleteEntity
{
    public bool IsDeleted { get; set; } = false;
    public DateTimeOffset? DeletedAt { get; set; }
}
