using Ayllu.Web.Domain.Entities.Common;
using Ayllu.Web.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayllu.Web.Domain.Entities.Dialectics;

public abstract class DialecticContribution : SoftDeleteEntity<string>
{
    [Required]
    public string Content { get; protected set; } = string.Empty;

    [Required]
    public string AuthorUserId { get; protected set; } = default!;

    public ApplicationUser Author { get; protected set; } = default!;

    [Required]
    public string DialecticId { get; protected set; } = null!;
    public Dialectic Dialectic { get; protected set; } = default!;
}