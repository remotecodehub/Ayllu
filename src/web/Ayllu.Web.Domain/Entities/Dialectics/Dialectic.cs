using Ayllu.Web.Domain.Entities.Common;
using Ayllu.Web.Domain.Entities.Groups;
using Ayllu.Web.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayllu.Web.Domain.Entities.Dialectics;

public class Dialectic : SoftDeleteEntity<string>
{
    [Required]
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; set; }
    [Required]
    public bool IsPublic { get; set; }
    public DialecticStatus Status { get; private set; }
    [Required]
    public string OwnerUserId { get; set; } = null!;
    public ApplicationUser OwnerUser { get; private set; } = default!;
    public Thesis Thesis { get; private set; } = default!;
    public virtual ICollection<Antithesis> Antitheses { get; private set; } = [];
    public virtual ICollection<Synthesis> Syntheses { get; private set; } = [];
    public virtual ApplicationGroupDialectic GroupContext { get; set; } = default!;

    protected Dialectic() { }

    public Dialectic(string title, string ownerUserId) 
    {
        Id = Guid.NewGuid().ToString();
        Title = title;
        OwnerUserId = ownerUserId;
        Status = DialecticStatus.Draft;
    }

    public void PublishThesis(Thesis thesis)
    {
        Thesis = thesis;
        Status = DialecticStatus.ThesisPublished;
    }

    public void OpenAntithesis()
    {
        Status = DialecticStatus.AntithesisOpen;
    }

    public void Close()
    {
        Status = DialecticStatus.Closed;
    }

    public void Store()
    {
        Status = DialecticStatus.Stored;
    }

    public void Archive()
    {
        Status = DialecticStatus.Archived;
    }

    public void Delete()
    {
        Status = DialecticStatus.Deleted;
    }
} 