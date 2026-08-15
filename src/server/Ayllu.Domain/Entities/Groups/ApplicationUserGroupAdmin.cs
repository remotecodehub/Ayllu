namespace Ayllu.Domain.Entities.Groups;

public class ApplicationUserGroupAdmin 
{
    public ApplicationUserGroupAdmin() { UserId = string.Empty; GroupId = string.Empty; }
    public ApplicationUserGroupAdmin(string userId, string groupId)
    {
        UserId = userId;
        GroupId = groupId;
    }
    public string UserId { get; set; }
    public string GroupId { get; set; }
}