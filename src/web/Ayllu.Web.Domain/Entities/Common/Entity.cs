using Ayllu.Web.Domain.Abstractions.Common;
using Ayllu.Web.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayllu.Web.Domain.Entities;

/// <summary>
/// Entidade que representa um token de acesso do usuário no GitHub
/// </summary>
public class Entity<TKey> : IEntity<TKey>
{
    /// <summary>
    /// Id da entidade, gerado na construção do objeto
    /// </summary>
    [Key]
    public TKey Id { get; set; } = default!;
    /// <summary>
    /// Gets or sets the date and time when the entity was created.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }
    /// <summary>
    /// Gets or sets the date and time when the entity was last updated. 
    /// </summary>
    public DateTimeOffset? UpdatedAt { get; set; }
    /// <summary>
    /// Gets or sets the timestamp used to manage optimistic concurrency for the entity.
    /// </summary>
    /// <remarks>This property is typically updated automatically to detect conflicting changes when multiple
    /// processes attempt to modify the same entity concurrently. It should be set to a new value each time the entity
    /// is updated to ensure proper concurrency control.</remarks>
    public Guid ConcurrencyTimestamp { get; set; } = Guid.NewGuid();
}