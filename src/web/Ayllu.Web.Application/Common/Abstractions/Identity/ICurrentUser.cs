using Ayllu.Web.Application.Identity.Dtos;

namespace Ayllu.Web.Application.Common.Abstractions.Identity;

public interface ICurrentUser
{
    Task<UserDto> GetUserAsync(CancellationToken cancellationToken);
}