using Ayllu.Web.Domain.Abstractions.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayllu.Web.Domain.Entities.Common;

public class SoftDeleteEntity<TKey> : Entity<TKey>, ISoftDeleteEntity
{
    public bool IsDeleted { get; set; } = false;
    public DateTimeOffset? DeletedAt { get; set; }
}
