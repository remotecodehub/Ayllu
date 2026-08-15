namespace Ayllu.Application.Identity.Responses;

public sealed class ApplicationUserFriendResponse(string currentUserId = "", string friendUserId = "", string status = "") : IResponse
{
    public string CurrendUserId { get; init; } = currentUserId;
    public string FriendUserId { get; init; } = friendUserId;
    public string Status { get; init; } = status;
}


public sealed class ApplicationUserFriendListResponse(ICollection<ApplicationUserFriendResponse> friends) : IResponse
{
    public ICollection<ApplicationUserFriendResponse> Friends { get; init; } = friends;
}
