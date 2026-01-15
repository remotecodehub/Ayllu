using Ayllu.Web.Application.Identity.Dtos;
using Ayllu.Web.Application.Identity.Requests;
using Ayllu.Web.Application.Identity.Responses;

namespace Ayllu.Web.Application.Common.Abstractions.Identity;

public interface IIdentityService
{
    Task<ApplicationUserResponse> GetCurrentUserAsync(string userId, CancellationToken cancellationToken);
    Task<ICollection<ApplicationUserFriendResponse>> GetUserFriendsAsync(string userId, CancellationToken cancellationToken);
    Task<ApplicationUserFriendResponse> SendInviteUserFriendAsync(string userId, string friendId, CancellationToken cancellationToken);
    Task<bool> LogoutAsync(string userId, CancellationToken cancellationToken);
    Task<ApplicationUserResponse> UpdateUserAsync(UpdateUserRequest request, string userId, CancellationToken cancellationToken);
}
