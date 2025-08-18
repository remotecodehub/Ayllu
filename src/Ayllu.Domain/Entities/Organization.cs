namespace Ayllu.Domain.Entities;

public class Organization
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? LogoUrl { get; set; }

    // Relacionamentos
    public ICollection<AppUserOrganization> Members { get; set; } = new List<AppUserOrganization>();
    public ICollection<Movement> Movements { get; set; } = new List<Movement>();
    public ICollection<Dialectic> Dialectics { get; set; } = new List<Dialectic>();
}
