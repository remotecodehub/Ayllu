using Microsoft.AspNetCore.Identity;

namespace Ayllu.Domain.Entities;

public class AppUser : IdentityUser<string>
{
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? ProfilePictureUrl { get; set; } = string.Empty;

    public ICollection<AppUserOrganization> OrganizationMemberships { get; } = new List<AppUserOrganization>();
    public ICollection<Movement> MovementsCreated { get; } = new List<Movement>();
    public ICollection<Dialectic> DialecticsCreated { get; } = new List<Dialectic>();
    public ICollection<DialecticStage> DialecticStagesAuthored { get; } = new List<DialecticStage>();
    public ICollection<UserConnection> ConnectionsRequested { get; } = new List<UserConnection>();
    public ICollection<UserConnection> ConnectionsReceived { get; } = new List<UserConnection>();
}
