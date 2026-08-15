namespace Ayllu.Application.Identity.Responses;

public sealed class ApplicationUserFriendResponse(string currentUserId = "", string friendUserId = "", string status = "")
{
    public string CurrendUserId { get; private set; } = currentUserId;
    public string FriendUserId { get; private set; } = friendUserId;
    public string Status { get; private set; } = status;
}
