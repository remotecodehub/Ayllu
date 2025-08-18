namespace Ayllu.Domain.Entities
{
    /// <summary>
    /// Representa uma etapa na discussão dialética (Tese, Antítese, Síntese, etc.)
    /// </summary>
    public class DialecticStage
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DialecticStageType StageType { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relacionamentos
        public string DialecticId { get; set; } = string.Empty;
        public Dialectic Dialectic { get; set; } = null!;

        public string AuthorId { get; set; } = string.Empty;
        public AppUser Author { get; set; } = null!;
    }
}