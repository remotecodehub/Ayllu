namespace Ayllu.Domain.Entities
{
    public class Dialectic
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relacionamentos
        public string OrganizationId { get; set; } = string.Empty;
        public Organization Organization { get; set; } = null!;

        public string CreatedById { get; set; } = string.Empty;
        public AppUser CreatedBy { get; set; } = null!;

        public ICollection<DialecticStage> Stages { get; set; } = new List<DialecticStage>();
    }
}