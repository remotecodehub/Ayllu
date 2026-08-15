using Ayllu.Domain.Entities.Common;
using Ayllu.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayllu.Domain.Entities.Groups;

/// <summary>
/// Grupo para discussoes entre usuarios, como em um chat ou forum
/// </summary>
public class ApplicationGroup : SoftDeleteEntity<string>
{
    [Required]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Required]
    public string CreatorId { get; set; } = string.Empty;

    public ApplicationUser Creator { get; set; } = default!;

    public ICollection<ApplicationUserGroupAdmin> Admins { get; set; } = new HashSet<ApplicationUserGroupAdmin>();
    public ICollection<ApplicationUserGroup> Members { get; set; } = new HashSet<ApplicationUserGroup>();
     
    public ICollection<ApplicationGroupDialectic> Dialectics { get; set; } = new HashSet<ApplicationGroupDialectic>();
}