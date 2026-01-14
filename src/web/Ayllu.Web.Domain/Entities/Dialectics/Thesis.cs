namespace Ayllu.Web.Domain.Entities.Dialectics;

public class Thesis : DialecticContribution
{
    protected Thesis() { }

    public Thesis(string content, string authorUserId, string dialecticId)
    {
        Id = Guid.NewGuid().ToString();
        Content = content;
        AuthorUserId = authorUserId;
        DialecticId = dialecticId;
    }

    public void UpdateContent(string content) => this.Content = content;
}