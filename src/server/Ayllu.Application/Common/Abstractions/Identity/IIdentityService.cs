using Ayllu.Application.Identity.Requests;
using Ayllu.Application.Identity.Responses;
using Ayllu.Application.Identity.Dtos;

namespace Ayllu.Application.Common.Abstractions.Identity;

public interface IIdentityService
{
    Task<ApplicationUserResponse> GetCurrentUserAsync(string userId, CancellationToken cancellationToken);
    Task<ICollection<ApplicationUserFriendResponse>> GetUserFriendsAsync(string userId, CancellationToken cancellationToken);
    Task<ApplicationUserFriendResponse> SendInviteUserFriendAsync(string userId, string friendId, CancellationToken cancellationToken);
    Task<bool> LogoutAsync(string userId, CancellationToken cancellationToken);
    Task<ApplicationUserResponse> UpdateUserAsync(UpdateUserRequest request, string userId, CancellationToken cancellationToken);
}
