using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Ayllu.Web.Domain.Abstractions.Common;

namespace Ayllu.Web.Infrastructure.Persistence.Extensions;

public static class DbSetExtensions
{
    public static async Task<EntityEntry<T>> AddAsync<T>(this DbSet<T> dbSet, T entity, CancellationToken cancellationToken) where T : class, IEntity<string>, new()
    {
        if (string.IsNullOrEmpty(entity.Id))
            entity.Id = Guid.NewGuid().ToString();
        return await dbSet.AddAsync(entity, cancellationToken);
    }
}
