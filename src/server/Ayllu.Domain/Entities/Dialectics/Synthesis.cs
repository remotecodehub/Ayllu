using Ayllu.Domain.Entities.Common;
using Ayllu.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayllu.Domain.Entities.Dialectics;

public class Synthesis : Entity<string>
{
    public string Content { get; private set; } = string.Empty;
    public string? DialecticId { get; private set; }
    public Dialectic? Dialectic { get; private set; }
    public string? ThesisId { get; private set; }
    public Thesis? Thesis { get; private set; }
    public string? AntithesisId { get; private set; }
    public Antithesis? Antithesis { get; private set; }

    public string AuthorUserId { get; private set; } = default!;
    public ApplicationUser AuthorUser { get; private set; } = default!;

    protected Synthesis() => Id = Guid.Empty.ToString();

    public Synthesis(string dialecticId, string authorUserId, string content)
    {
        AuthorUserId = authorUserId;
        Content = content;
        DialecticId = dialecticId;
        Id = Guid.NewGuid().ToString();
    }
}
