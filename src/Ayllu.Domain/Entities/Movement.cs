namespace Ayllu.Domain.Entities
{
    public class Movement
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relacionamentos
        public string OrganizationId { get; set; } = string.Empty;
        public Organization Organization { get; set; } = null!;

        public string CreatedById { get; set; } = string.Empty;
        public AppUser CreatedBy { get; set; } = null!;
    }
}