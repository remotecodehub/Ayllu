namespace Ayllu.Web.Domain.Entities.Dialectics;

public class Antithesis : DialecticContribution
{
    protected Antithesis() { }

    public Antithesis(string content, string authorUserId, string dialecticId)
    {
        AuthorUserId = authorUserId;
        Content = content;
        DialecticId = dialecticId;
        Id = Guid.NewGuid().ToString();
    }
}