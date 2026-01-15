using Ayllu.Web.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayllu.Web.Domain.Entities.Groups;

/// <summary>
/// Grupo de usuarios
/// </summary>
public class ApplicationUserGroup
{
    /// <summary>
    /// Id do usuario pertencente ao grupo
    /// </summary>
    [Required]
    public string UserId { get; set; } = null!;

    /// <summary>
    /// Usuario pertencente ao grupo
    /// </summary>
    [ForeignKey(nameof(UserId))]
    public virtual ApplicationUser User { get; set; } = default!;

    /// <summary>
    /// Id do grupo pertencente ao usuario 
    /// </summary>
    public string? GroupId { get; set; } = null!;

    /// <summary>
    /// Gruoo do user
    /// </summary>
    public ApplicationGroup? Group { get; set; } = default!;
}
