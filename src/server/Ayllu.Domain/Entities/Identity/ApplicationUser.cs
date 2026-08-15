using Ayllu.Domain.Entities.Dialectics;
using Ayllu.Domain.Entities.Groups;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayllu.Domain.Entities.Identity;

/// <summary>
/// Usuario para a aplicação
/// </summary>
public class ApplicationUser : IdentityUser<string>
{
    public ApplicationUser() : base()
    {
        Id = Guid.NewGuid().ToString();
    }
    
    public ApplicationUser(string userName) : base(userName)
    {
        Id = Guid.NewGuid().ToString();
    }
    
    /// <summary>
    /// Nome
    /// </summary>
    public string? Name { get; set; } = string.Empty;

    /// <summary>
    /// Idioma do usuário
    /// </summary>
    [MaxLength(11)]
    public string? Culture { get; set; } = string.Empty;
    
    /// <summary>
    /// Sobrenome
    /// </summary>
    public string? LastName { get; set; } = string.Empty; 
    
    /// <summary>
    /// Relacionamentos de amizade iniciados
    /// </summary>
    public virtual ICollection<ApplicationUserFriendship>? FriendshipsInitiated { get; set; } = [];
    
    /// <summary>
    /// Relacionamentos de amizade recebidos
    /// </summary>
    public virtual ICollection<ApplicationUserFriendship>? FriendshipsReceived { get; set; } = [];

    /// <summary>
    /// Propriedade auxiliar para acessar todos os amigos
    /// </summary>
    [NotMapped]
    public IEnumerable<ApplicationUser> Friends =>
        FriendshipsInitiated?.Select(f => f.UserB)
        .Concat(FriendshipsReceived?.Select(f => f.UserA) ?? []) ?? [];

    /// <summary>
    /// Gets or sets the collection of dialectics owned by this entity.
    /// </summary>
    public virtual ICollection<Dialectic>? OwnedDialectics { get; set; } = [];
    
    /// <summary>
    /// Gets or sets the collection of theses authored by this entity.
    /// </summary>
    public virtual ICollection<Thesis>? AuthoredTheses { get; set; } = [];
    
    /// <summary>
    /// Gets or sets the collection of antitheses authored by this entity.
    /// </summary>
    public virtual ICollection<Antithesis>? AuthoredAntitheses { get; set; } = [];
    
    /// <summary>
    /// Gets or sets the collection of syntheses authored by the user.
    /// </summary>
    public virtual ICollection<Synthesis>? AuthoredSyntheses { get; set; } = [];
    /// <summary>
    /// Gets or sets the collection of groups that were created by this user.
    /// </summary>
    public virtual ICollection<ApplicationGroup>? CreatedGroups { get; set; } = [];
    /// <summary>
    /// Gets or sets the collection of the group dialectics created by this user.
    /// </summary>
    public virtual ICollection<ApplicationGroupDialectic> GroupDialecticsCreated { get; set; } = [];
    /// <summary>
    /// Gets or sets the groups which the user belongs
    /// </summary>
    public virtual ICollection<ApplicationUserGroup> Groups { get; set; } = [];
    /// <summary>
    /// Gets or sets the groups which the user is administrator
    /// </summary>
    public virtual ICollection<ApplicationUserGroupAdmin> AdminGroups { get; set; } = [];
}
