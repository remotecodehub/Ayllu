using Ayllu.Application.Identity.Dtos;

namespace Ayllu.Application.Common.Abstractions.Identity;

public interface ICurrentUser
{
    Task<UserDto> GetUserAsync(CancellationToken cancellationToken);
}