using Ayllu.Web.Domain.Entities.Common;
using Ayllu.Web.Domain.Entities.Dialectics;
using Ayllu.Web.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayllu.Web.Domain.Entities.Groups;

public class ApplicationGroupDialectic : SoftDeleteEntity<string>
{
    [Required]
    public string DialecticId { get; set; } = null!;
    
    [ForeignKey(nameof(DialecticId))]
    public Dialectic Dialectic { get; set; } = default!;

    [Required]
    public string GroupId { get; set; } = default!;

    [ForeignKey(nameof(GroupId))]
    public ApplicationGroup Group { get; set; } = default!;

    public GroupDialecticVisibility Visibility { get; set; }

    public string CreatedByUserId { get; set; } = default!;
    
    [ForeignKey(nameof(CreatedByUserId))]
    public ApplicationUser CreatedBy { get; set; } = default!;
}
