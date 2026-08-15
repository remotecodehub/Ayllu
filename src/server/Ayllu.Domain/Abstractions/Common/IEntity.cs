namespace Ayllu.Domain.Abstractions.Common;

public interface IEntity<TKey>
{
    TKey Id { get; set; }
    DateTimeOffset CreatedAt { get; set; }
    DateTimeOffset? UpdatedAt { get; set; }
    Guid ConcurrencyTimestamp { get; set; }
}
