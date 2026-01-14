using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayllu.Web.Domain.Entities.Identity;

/// <summary>
/// Classe para vínculo de amizade entre dois usuários
/// </summary>
public class ApplicationUserFriendship
{
    /// <summary>
    /// Id do usuario iniciador do vinculo de amizade
    /// </summary>
    public string UserAId { get; set; } = null!;

    /// <summary>
    /// Id do usuario receptor do vinculo de amizade
    /// </summary>
    public string UserBId { get; set; } = null!;

    /// <summary>
    /// Usuario iniciador do vinculo de amizade
    /// </summary>
    public virtual ApplicationUser UserA { get; set; } = default!;

    /// <summary>
    /// Usuario receptor do vinculo de amizade
    /// </summary>
    public virtual ApplicationUser UserB { get; set; } = default!;

    /// <summary>
    /// Gets or sets the current friendship status between the user and another user.
    /// </summary>
    /// <remarks>Use essa propriedade para determinar ou atualizar o estado de relacionamento, como por exemplo se o estado da amizade está como
    /// 'amigos', 'pendente de aprovação', ou 'bloqueado'. Os valores correspondentes sao membros da seguinte enumeração: <see
    /// cref="UserFriendshipStatus"/>.</remarks>
    public UserFriendshipStatus Status { get; set; } = UserFriendshipStatus.DEFAULT;
    
    protected ApplicationUserFriendship() { }
    public ApplicationUserFriendship(string userAId, string userBId, UserFriendshipStatus status = UserFriendshipStatus.DEFAULT)
    {
        UserAId = userAId;
        UserBId = userBId;
        Status = status;
    }
}
