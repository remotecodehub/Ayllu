using Ayllu.Domain.Entities.Identity;
using System.Linq.Expressions;

namespace Ayllu.Application.Common.Abstractions.Identity;

public interface IApplicationUserFriendshipRepository
{
    IQueryable<ApplicationUserFriendship> Query();
    Task<ApplicationUserFriendship> CreateAsync(ApplicationUserFriendship entity, CancellationToken cancellationToken);
    Task<ApplicationUserFriendship> UpdateAsync(ApplicationUserFriendship entity, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(ApplicationUserFriendship entity, CancellationToken cancellationToken);
    Task<bool> AnyAsync(Expression<Func<ApplicationUserFriendship, bool>> predicate, CancellationToken cancellationToken);
}
