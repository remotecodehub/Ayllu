using Ayllu.Application.Common.Abstractions.Storage;
using Ayllu.Application.Common.Abstractions.UnitOfWork;
using Ayllu.Domain.Abstractions.Common;
using Ayllu.Infrastructure.Persistence.Data;
using Ayllu.Domain.Exceptions.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Ayllu.Infrastructure.Common.Repositories.Common;

/// <summary>
/// Generic repository implementation for managing entities in the database.
/// </summary>
/// <param name="db"><see cref="ApplicationDbContext"/> instance</param>
/// <param name="logger"><see cref="ILogger{UserTokenRepository}"/> instance</param>
public sealed class Repository<T>(ApplicationDbContext db, IUnitOfWork uow, ILogger<Repository<T>> logger) : IRepository<T> where T : class, IEntity<string>
{
    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        try
        {
            return await db.Set<T>().AnyAsync(predicate, cancellationToken);
        }
        catch (Exception e)
        {
            logger.LogError(e, "{Message}", e.Message);
            throw;
        }
    }
    public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken)
    {
        try
        {
            var entry = await db.Set<T>().AddAsync(entity, cancellationToken);
            if (entry.State != EntityState.Added)
            {
                throw new RepositoryException("Entity could not be added to the context.");
            }
            var result = await uow.SaveChangesAsync(cancellationToken);
            if (result <= 0)
            {
                throw new RepositoryException("No changes were saved to the database.");
            }
            return entry.Entity;
        }
        catch (Exception e)
        {
            logger.LogError(e, "{Message}", e.Message);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken)
    {
        try
        {
            var entry = db.Set<T>().Remove(entity);
 
            _ = await uow.SaveChangesAsync(cancellationToken);

            return true;

        }
        catch (Exception e)
        {
            logger.LogError(e, "{Message}", e.Message);
            throw;
        }
    }
    public IQueryable<T> Query()
    {
        try
        {
            return db.Set<T>();
        }
        catch (Exception e)
        {
            logger.LogError(e, "{Message}", e.Message);
            throw;
        }
    }

    public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        try
        {
            var entry = db.Set<T>().Update(entity);
            if (entry.State != EntityState.Modified)
            {
                throw new RepositoryException("Entity could not be updated on the context.");
            }
            var result = await uow.SaveChangesAsync(cancellationToken);
            if (result <= 0)
            {
                throw new RepositoryException("No changes were saved to the database.");
            }
            return entry.Entity;
        }
        catch (Exception e)
        {
            logger.LogError(e, "{Message}", e.Message);
            throw;
        }
    }
}
